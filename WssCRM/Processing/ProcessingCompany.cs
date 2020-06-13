using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WssCRM.Models;

namespace WssCRM.Processing
{
    public class ProcessingCompany
    {
        DBModels.ApplicationContext db;
        public ProcessingCompany(DBModels.ApplicationContext context)
        {
            db = context;
        }
        public Company ParsingXLS(IFormFile file)
        {

            Company company = new Company();
            company.stages = new List<Stage>();
            using (var stream = file.OpenReadStream())
            {
                XLWorkbook wb = new XLWorkbook(stream);
                foreach (var page in wb.Worksheets.Where(p => p.Name.ToUpper().Trim() != "СТАТИСТИКА" && p.Name.ToUpper().Trim() != "СВОДНАЯ" && p.Name.ToUpper().Trim() != "СТАТИСТИКИ"))
                {
                    Stage s1 = new Stage(page.Name, 0, false);
                    s1.points = new List<Point>();
                    int corrRow = 5;
                    while (!Regex.Match(page.Cell(corrRow, 1).GetString(), "коррекции", RegexOptions.IgnoreCase).Success)
                    {
                        corrRow++;
                    }

                    IXLCell CellPoint = page.Cell("D5");

                    while (CellPoint.Address.RowNumber < corrRow - 4)
                    {
                        Point point = new Point();
                        point.Name = CellPoint.GetString();
                        int maxMark;
                        if (page.Cell("B" + CellPoint.Address.RowNumber).TryGetValue<int>(out maxMark))
                        {
                            point.maxMark = maxMark;
                            s1.points.Add(point);
                        }
                        CellPoint = CellPoint.CellBelow();
                    }
                    company.stages.Add(s1);
                }

            }
            return company;
        }
        public Company GetCompany(int id)
        {
            DBModels.Company dbcomp;
            try
            {
                dbcomp = db.Companies.Where(c => c.Id == id).First();
                dbcomp.Stages = db.Stages.Where(s => s.CompanyID == dbcomp.Id && s.deleted != true).ToList();
                foreach (var s in dbcomp.Stages)
                {
                    dbcomp.Stages.Where(st => st == s).First().Points =
                        db.AbstractPoints.Where(p => p.StageID == s.Id).ToList();
                }
                dbcomp.Managers = db.Managers.Where(m => m.CompanyID == dbcomp.Id && m.deleted != true).ToList();
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
                returnCompany.managers.Add(new Manager(dbman.name, dbman.Id, false));
            }
            return returnCompany;
        }

        public string AddCompany(Company clientComp)
        {
            DBModels.Company dbcomp = new DBModels.Company();
            dbcomp.Id = clientComp.id;
            dbcomp.name = clientComp.Name;
            using (var transaction = db.Database.BeginTransaction())
            {
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

                    transaction.Rollback();
                    return "Имя компании должно быть уникально";
                }
                foreach (var stage in clientComp.stages)
                {
                    DBModels.Stage dbStage = new DBModels.Stage();
                    dbStage.Name = stage.name;
                    dbStage.Id = stage.id;
                    dbStage.CompanyID = db.Companies.Where(c => c.name == clientComp.Name).First().Id;
                    dbStage.deleted = stage.deleted;
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

                    transaction.Rollback();
                    return "Имя этапов должно быть уникально";
                }
                foreach (var man in clientComp.managers)
                {
                    DBModels.Manager dbman = new DBModels.Manager();
                    dbman.Id = man.id;
                    dbman.name = man.name;
                    dbman.CompanyID = db.Companies.Where(c => c.name == clientComp.Name).First().Id;
                    dbman.deleted = man.deleted;
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

                    transaction.Rollback();
                    return "Имя менеджеров должно быть уникально";
                }

                foreach (var stage in clientComp.stages)
                {
                    DBModels.Stage dbStage = db.Stages.Where(s => s.Name == stage.name).First();

                    foreach (var point in stage.points)
                    {
                        DBModels.AbstractPoint dbPoint = new DBModels.AbstractPoint();
                        dbPoint.name = point.Name;
                        dbPoint.maxMark = point.maxMark;
                        dbPoint.Id = point.id;
                        dbPoint.StageID = dbStage.Id;
                        dbPoint.deleted = point.deleted;
                        if (db.AbstractPoints.Contains(dbPoint))
                        {
                            db.AbstractPoints.Update(dbPoint);
                        }
                        else
                        {
                            db.AbstractPoints.Add(dbPoint);
                        }

                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbUpdateException)
                        {

                            transaction.Rollback();
                            return "Имена пунктов в этапе '" + dbStage.Name + " " + dbPoint.name + "' должны быть уникальными";
                        }

                    }
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        transaction.Rollback();
                        return "Имена пунктов в этапе '" + dbStage.Name + "' должны быть уникальными";
                    }
                }
                transaction.Commit();
                return "";
            }
         }
            
    }

}
