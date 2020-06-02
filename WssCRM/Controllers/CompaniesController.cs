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
        [HttpDelete("{id}")]
        public IActionResult DeleteCompany (int id)
        {
            try
            {
                db.Companies.Remove(db.Companies.Where(c => c.Id == id).First());
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("can'tdel", "Не удалось удалить");
                return BadRequest(ModelState);
            }
            return Ok();
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
                dbcomp.Managers = db.Managers.Where(m => m.CompanyID == dbcomp.Id).ToList();
            }
            catch
            {
                return new Company();
            }
            Company returnCompany = new Company(dbcomp.name, dbcomp.Id);
            returnCompany.stages = new List<Stage>();
            returnCompany.managers = new List<Manager>();
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
            foreach (var dbman in dbcomp.Managers)
            {
                returnCompany.managers.Add(new Manager(dbman.name, dbman.Id));
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
                if (db.Companies.Contains(dbcomp))
                {
                    db.Companies.Update(dbcomp);
                }
                else
                {
                    db.Companies.Add(dbcomp);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("key", "Имя компании должно быть уникально");
                    return BadRequest(ModelState);
                }
                foreach (var stage in clientComp.stages)
                {
                    DBModels.Stage dbStage = new DBModels.Stage();
                    dbStage.Name = stage.name;
                    dbStage.Id = stage.id;
                    dbStage.CompanyID = db.Companies.Where(c => c.name == clientComp.Name).First().Id;
                    if (db.Stages.Contains(dbStage))
                    {
                        db.Stages.Update(dbStage);
                    }
                    else
                    {
                        db.Stages.Add(dbStage);
                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("key", "Имя этапов должно быть уникально");
                    return BadRequest(ModelState);
                }
                foreach (var man in clientComp.managers)
                {
                    DBModels.Manager dbman = new DBModels.Manager();
                    dbman.Id = man.id;
                    dbman.name = man.name;
                    dbman.CompanyID = db.Companies.Where(c => c.name == clientComp.Name).First().Id;
                    if (db.Managers.Contains(dbman))
                    {
                        db.Managers.Update(dbman);
                    }
                    else
                    {
                        db.Managers.Add(dbman);
                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("key", "Имя менеджеров должно быть уникально");
                    return BadRequest(ModelState);
                }

                foreach (var stage in clientComp.stages)
                {
                    DBModels.Stage dbStage = db.Stages.Where(s => s.Name == stage.name).First();
                    
                    foreach(var point in stage.points)
                    {
                        DBModels.AbstractPoint dbPoint = new DBModels.AbstractPoint();
                        dbPoint.name = point.Name;
                        dbPoint.maxMark = point.maxMark;
                        dbPoint.Id = point.id;
                        dbPoint.StageID = dbStage.Id;
                        if (db.AbstractPoints.Contains(dbPoint))
                        {
                            db.AbstractPoints.Update(dbPoint);
                        }
                        else
                        {
                            db.AbstractPoints.Add(dbPoint);
                        }
                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError("key", "Имена пунктов в этапе '" + dbStage.Name + "' должны быть уникальными");
                        return BadRequest(ModelState);
                    }
                }
                
                
                return Ok(clientComp);
            }
            ModelState.AddModelError("other", "Что-то пошло не так, обратитесь к разработчикам");
            return BadRequest(ModelState);
        }
    }
}