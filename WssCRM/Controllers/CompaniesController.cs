using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WssCRM.Models;
namespace WssCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private DBModels.ApplicationContext db;
        public CompaniesController(DBModels.ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public IEnumerable<Company> GetCompanies()
        {
            List<Company> Companies = new List<Company>();
            foreach (var comp in db.Companies)
            {
                Companies.Add(new Company(comp.name,comp.Id));
            }
            return Companies;
        }
        [HttpGet("{id}")]
        public Company GetCompany(int id)
        {
            DBModels.Company dbcomp;
            try
            {
                dbcomp = db.Companies.Where(c => c.Id == id).First();
                dbcomp.Stages = db.Stages.Where(s => s.CompanyID == dbcomp.Id).ToList();
                foreach (var s in dbcomp.Stages)
                {
                    dbcomp.Stages.Where(st => st == s).First().Points =
                        db.AbstractPoints.Where(p => p.StageID == s.Id).ToList();
                }
            }
            catch
            {
                return new Company();
            }
            Company returnCompany = new Company(dbcomp.name, dbcomp.Id);
                returnCompany.stages = new List<Stage>();
            foreach (var dbstage in dbcomp.Stages)
            {
                Stage curStage = new Stage();
                curStage.name = dbstage.Name;
                curStage.points = new List<Point>();
                curStage.id = dbstage.Id;
                foreach (var dbpoint in dbstage.Points)
                {
                    Point p = new Point();
                    p.Name = dbpoint.name;
                    p.maxMark = dbpoint.maxMark;
                    p.id = dbpoint.Id;
                    curStage.points.Add(p);
                }
                returnCompany.stages.Add(curStage);
            }
            return returnCompany;
            
            
        }
        [HttpPost]
        public IActionResult AddCompany(Company clientComp)
        {
            if (ModelState.IsValid)
            {
                DBModels.Company dbcomp = new DBModels.Company();
                dbcomp.Id = clientComp.id;
                dbcomp.name = clientComp.Name;
                dbcomp.Stages = new List<DBModels.Stage>();
                foreach (var stage in clientComp.stages)
                {
                    DBModels.Stage dbStage = new DBModels.Stage();
                    dbStage.Name = stage.name;
                    dbStage.Points = new List<DBModels.AbstractPoint>();
                    dbStage.Id = stage.id;
                    foreach(var point in stage.points)
                    {
                        DBModels.AbstractPoint dbPoint = new DBModels.AbstractPoint();
                        dbPoint.name = point.Name;
                        dbPoint.maxMark = point.maxMark;
                        dbPoint.Id = point.id;
                        dbStage.Points.Add(dbPoint);
                    }
                    dbcomp.Stages.Add(dbStage);
                }

                //db.Database.Migrate();
                if (db.Companies.Contains(dbcomp))
                {
                    
                    foreach(var dbstage in dbcomp.Stages)
                    {
                        if (db.Stages.Contains(dbstage))
                        {
                            //foreach (var dbpoint in dbstage.Points)
                            //{
                            //    if (db.AbstractPoints.Contains(dbpoint))
                            //    {
                            //        db.AbstractPoints.Update(dbpoint);
                            //    }
                            //    else
                            //    {
                            //        dbpoint.StageID = dbstage.Id;
                            //        db.AbstractPoints.Add(dbpoint);
                            //        //
                            //    }
                            //}
                            dbstage.CompanyID = dbcomp.Id;
                            db.Stages.Update(dbstage);
                            db.SaveChanges();
                        }
                        else
                        {
                            //dbstage.CompanyID = dbcomp.Id;
                            db.Stages.Add(dbstage);
                            
                            
                        }
                    }
                    db.Companies.Update(dbcomp);
                    
                }
                else
                {
                    db.Companies.Add(dbcomp);
                    
                }
                db.SaveChanges();
                return Ok(clientComp);
            }
            return BadRequest(ModelState);
        }
    }
}