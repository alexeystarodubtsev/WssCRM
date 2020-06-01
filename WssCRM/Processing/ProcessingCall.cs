using System;
using System.Collections.Generic;
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
                call.Company.stages.Add(new Stage(dbstage.Name, dbstage.Id));
                call.Stage = new Stage(dbstage.Name, dbstage.Id);
                call.Date = dbcall.Date;
                call.duration = dbcall.duration;
                call.Correction = dbcall.Correction;
                call.ClientName = dbcall.ClientName;
                call.ClientLink = dbcall.ClientLink;
                foreach (var dbpoint in db.Points.Where(p=>p.CallID == dbcall.Id))
                {
                    var dbAbstractPoint = db.AbstractPoints.Where(p => p.Id == dbpoint.AbstractPointID).First();
                    call.points.Add(new Point(dbAbstractPoint.name, dbpoint.Value, dbAbstractPoint.maxMark,dbAbstractPoint.Id));
                }
            }
            catch (System.InvalidOperationException e)
            {
                return new Call();
            }
           

            return call;
        }
        public Call NewCall(int CompanyID, int StageID)
        {

            Call call = new Call();
            var dbcomp = db.Companies.Where(c => c.Id == CompanyID).First();
            var dbstage = db.Stages.Where(s => s.Id == StageID).First();
            call.Company = new Company(dbcomp.name, dbcomp.Id);
            call.Company.stages = new List<Stage>();
            call.Company.stages.Add(new Stage(dbstage.Name, dbstage.Id));
            call.Stage = new Stage(dbstage.Name, dbstage.Id);
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                call.Date = DateTime.Now.AddDays(-3);
            }
            else
                call.Date = DateTime.Now.AddDays(-1);
            
            
            foreach (var dbpoint in db.AbstractPoints.Where(p => p.StageID == StageID))
            {
                call.points.Add(new Point(dbpoint.name,0,dbpoint.maxMark,dbpoint.Id));
            }
            

            return call;
        }
        
        public Filter getFilter(string opt)
        {
            Filter F1 = new Filter();
            Company company1;
            foreach (var dbcomp in db.Companies)
            {
                company1 = new Company(dbcomp.name, dbcomp.Id);
                foreach (var dbstage in db.Stages.Where(s => s.CompanyID == dbcomp.Id))
                {
                    company1.stages.Add(new Stage(dbstage.Name, dbstage.Id));
                }
                if (opt.ToUpper() == "ALL")
                  company1.stages.Add(new Stage("Все этапы", -40));
                foreach (var dbman in db.Managers.Where(m => m.CompanyID == dbcomp.Id))
                {
                    company1.managers.Add(new Manager(dbman.name, dbman.Id));
                }
                if (opt.ToUpper() == "ALL")
                    company1.managers.Add(new Manager("Все менеджеры", -40));
                F1.Companies.Add(company1);
            }
            return F1;
        }
    }
}
