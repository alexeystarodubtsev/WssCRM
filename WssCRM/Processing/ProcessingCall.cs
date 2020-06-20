using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WssCRM.Models;

namespace WssCRM.Processing
{
    public class ProcessingCall
    {
        DBModels.ApplicationContext db;
        public ProcessingCall(DBModels.ApplicationContext context)
        {
            db = context;
        }

        public Call GetCall(int id)
        {

            Call call = new Call();
            try
            {
                var dbcall = db.Calls.Where(c => c.Id == id).First();
                call.id = id;
                
                
                var dbstage = db.Stages.Where(s => s.Id == dbcall.StageID).First();
                var dbcomp = db.Companies.Where(c => c.Id == dbstage.CompanyID).First();

                call.Company = new Company(dbcomp.name, dbcomp.Id);
                call.Company.stages = new List<Stage>();
                call.Company.stages.Add(new Stage(dbstage.Name, dbstage.Id,dbstage.deleted));
                call.Stage = new Stage(dbstage.Name, dbstage.Id, dbstage.deleted);
                call.Date = dbcall.Date;
                call.duration = dbcall.duration;
                call.Correction = dbcall.Correction;
                call.ClientName = dbcall.ClientName;
                call.ClientLink = dbcall.ClientLink;
                call.clientState = dbcall.ClientState;
                call.correctioncolor = dbcall.correctioncolor;
                call.duration = dbcall.duration;
                call.hasDateNext = false;
                call.firstCalltoClient = dbcall.firstCalltoClient;
                if (dbcall.DateNext.HasValue)
                {
                    call.dateNext = dbcall.DateNext.Value;
                    call.hasDateNext = true;
                }
                call.manager = new Manager(db.Managers.Where(m => m.Id == dbcall.ManagerID).First().name, db.Managers.Where(m => m.Id == dbcall.ManagerID).First().Id, db.Managers.Where(m => m.Id == dbcall.ManagerID).First().deleted);
                foreach (var dbpoint in db.Points.Where(p=>p.CallID == dbcall.Id))
                {
                    var dbAbstractPoint = db.AbstractPoints.Where(p => p.Id == dbpoint.AbstractPointID).First();
                    call.points.Add(new Point(dbAbstractPoint.name, dbpoint.Value, dbAbstractPoint.maxMark,dbAbstractPoint.Id,false));
                }
            }
            catch (System.InvalidOperationException e)
            {
                return new Call();
            }
           

            return call;
        }


        public string UpdateCall(Call clientcall)
        {
            DBModels.Call dbcall = getDbCall(clientcall);
            dbcall.Points = new List<DBModels.Point>();
            foreach (var dbp in db.Points.Where(p => p.CallID == clientcall.id))
            {
                dbcall.Points.Add(dbp);
            };
            dbcall.Id = clientcall.id;
            db.Calls.Update(dbcall);
            try
            {
                db.SaveChanges();
            } 
            catch (DbUpdateException e)
            {
                return "Что-то пошло не так";
            }
            return "";
        }

        public Call NewCall(int CompanyID, int StageID)
        {

            Call call = new Call();
            var dbcomp = db.Companies.Where(c => c.Id == CompanyID).First();
            var dbstage = db.Stages.Where(s => s.Id == StageID).First();
            call.Company = new Company(dbcomp.name, dbcomp.Id);
            call.Company.stages = new List<Stage>();
            call.Company.stages.Add(new Stage(dbstage.Name, dbstage.Id,dbstage.deleted));
            call.Stage = new Stage(dbstage.Name, dbstage.Id, dbstage.deleted);
            call.correctioncolor = "no color";
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                call.Date = DateTime.Now.AddDays(-3);
            }
            else
                call.Date = DateTime.Now.AddDays(-1);
            
            
            foreach (var dbpoint in db.AbstractPoints.Where(p => p.StageID == StageID && p.deleted != true))
            {
                call.points.Add(new Point(dbpoint.name,0,dbpoint.maxMark,dbpoint.Id,false));
            }
            

            return call;
        }
        private DBModels.Call getDbCall(Call clientcall)
        {
            DBModels.Call dbcall = new DBModels.Call();
            dbcall.ClientName = clientcall.ClientName;
            dbcall.ClientLink = clientcall.ClientLink;
            dbcall.ClientState = clientcall.clientState;
            dbcall.Correction = clientcall.Correction;
            dbcall.correctioncolor = clientcall.correctioncolor;
            dbcall.Date = new DateTime();
            dbcall.Date = clientcall.Date.Date;
            dbcall.duration = clientcall.duration;
            dbcall.firstCalltoClient = clientcall.firstCalltoClient;
            if (clientcall.clientState == "Work" && clientcall.dateNext.Year > 2000)
            {
                dbcall.DateNext = clientcall.dateNext;
            }
            else
            {
                if (clientcall.clientState != "" && clientcall.clientState != "Work")
                {
                    dbcall.DateOfClose = clientcall.Date;
                }
            }
            dbcall.StageID = clientcall.Stage.id;
            dbcall.ManagerID = clientcall.manager.id;
            dbcall.Points = new List<DBModels.Point>();
            foreach (var point in clientcall.points)
            {
                DBModels.Point dbpoint = new DBModels.Point();
                dbpoint.AbstractPointID = point.id;
                dbpoint.Value = point.Value;
                dbcall.Points.Add(dbpoint);
            }
            return dbcall;
        }
        public string AddNewCall(Call clientcall)
        {

            DBModels.Call dbcall = getDbCall(clientcall);
            db.Calls.Add(dbcall);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                return "Что-то пошло не так";
            }
            return "";
        }
        public PartialCalls GetCalls(ChooseFilter f1)
        {
            PartialCalls l = new PartialCalls();
            l.calls  = new List<Call>();

           

            int qty = db.Calls.Count(c =>
                (c.StageID == f1.stage.id || f1.stage.id == -40)
                && c.Date >= f1.StartDate
                && c.Date <= f1.EndDate
                && (c.ManagerID == f1.manager.id || f1.manager.id == -40)
                && (db.Stages.Where(s => s.CompanyID == f1.Company.id && s.Id == c.StageID).Count() > 0)
                && (db.Managers.Where(m => m.CompanyID == f1.Company.id && m.Id == c.ManagerID).Count() > 0));
            int qtyonsamepage = 20;
            l.pageSize = (qty / qtyonsamepage) + ((qty % qtyonsamepage) > 0 ? 1 : 0);
            
            foreach (var dbcall in db.Calls.Where(c => 
                (c.StageID == f1.stage.id || f1.stage.id == -40)
                && c.Date >= f1.StartDate 
                && c.Date <= f1.EndDate
                && (c.ManagerID == f1.manager.id || f1.manager.id == -40)
                && (db.Stages.Where(s => s.CompanyID == f1.Company.id && s.Id == c.StageID).Count() > 0)
                && (db.Managers.Where(m => m.CompanyID == f1.Company.id && m.Id == c.ManagerID).Count() > 0)
            ).Skip((f1.pageNumber - 1)*qtyonsamepage).Take(qtyonsamepage))
            {
                Call call = new Call();
                call.ClientName = dbcall.ClientName;
                call.ClientLink = dbcall.ClientLink;
                call.id = dbcall.Id;
                call.Company = new Company(db.Companies.Where(c => c.Id == f1.Company.id).First().name, f1.Company.id);
                call.Stage = new Stage(db.Stages.Where(s => s.Id == dbcall.StageID).First().Name, db.Stages.Where(s => s.Id == dbcall.StageID).First().Id,false);
                call.manager = new Manager(db.Managers.Where(m => m.Id == dbcall.ManagerID).First().name, db.Managers.Where(m => m.Id == dbcall.ManagerID).First().Id, false);
                call.Date = dbcall.Date;
                l.calls.Add(call);
            }

            return l;
        }
        public Filter getFilter(string opt)
        {
            Filter F1 = new Filter();
            Company company1;
            foreach (var dbcomp in db.Companies)
            {
                company1 = new Company(dbcomp.name, dbcomp.Id);
                foreach (var dbstage in db.Stages.Where(s => s.CompanyID == dbcomp.Id && s.deleted != true))
                {
                    company1.stages.Add(new Stage(dbstage.Name, dbstage.Id,false));
                }
                if (opt.ToUpper() == "ALL")
                  company1.stages.Add(new Stage("Все этапы", -40, false));
                foreach (var dbman in db.Managers.Where(m => m.CompanyID == dbcomp.Id && m.deleted != true))
                {
                    company1.managers.Add(new Manager(dbman.name, dbman.Id,false));
                }
                if (opt.ToUpper() == "ALL")
                    company1.managers.Add(new Manager("Все менеджеры", -40,false));
                F1.Companies.Add(company1);
            }
            return F1;
        }
    }
}
