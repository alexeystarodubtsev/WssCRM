using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            dbcall.DateCreate = DateTime.Now;
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

        public string ProcessingCalls(int companyID, IFormFileCollection files)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                foreach (var file in files)
                {
                    DBModels.Manager dbman;
                    string managername = Regex.Match(file.FileName, @"(\w+)").Groups[1].Value;
                    try
                    {
                        dbman = db.Managers.Where(m => m.name == managername && m.CompanyID == companyID).First();

                    }
                    catch (InvalidOperationException)
                    {
                        return "У данной компании отсутствует менеджер " + managername;
                    }
                    string ans = parseFileWithCalls(dbman.Id, file);
                    if (ans != "")
                    {
                        transaction.Rollback();
                        return ans;
                    }

                }
                transaction.Commit();
            }
                return "";
        }
        public string parseFileWithCalls(int idManager, IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                XLWorkbook wb = new XLWorkbook(stream);
                
                    foreach (var page in wb.Worksheets.Where(p => !Regex.Match(p.Name,"статистик", RegexOptions.IgnoreCase).Success && !Regex.Match(p.Name, "сводн", RegexOptions.IgnoreCase).Success))
                    {
                        const int numColPoint = 4;
                        IXLCell CellDate = page.Cell(1, numColPoint + 1);
                        DateTime curDate;
                        DateTime.TryParse(CellDate.GetValue<string>(), out curDate);
                        Regex rComment = new Regex(@"КОРРЕКЦИИ");
                        int corrRow = 5;
                        Match Mcomment = rComment.Match(page.Cell(corrRow, 1).GetString().ToUpper());
                        while (!Mcomment.Success)
                        {
                            corrRow++;
                            Mcomment = rComment.Match(page.Cell(corrRow, 1).GetString().ToUpper());
                        }
                        while (!(CellDate.CellBelow().IsEmpty() && CellDate.CellBelow().CellRight().IsEmpty() && CellDate.CellBelow().CellBelow().IsEmpty() && CellDate.CellBelow().CellBelow().CellRight().IsEmpty()))
                        {
                            var curCall = new DBModels.Call();
                            if (CellDate.GetValue<string>() != "")
                            {
                                if (CellDate.DataType == XLDataType.DateTime)
                                    curDate = CellDate.GetDateTime();
                                else
                                {
                                    if (!DateTime.TryParse(CellDate.GetString(), new CultureInfo("ru-RU"), DateTimeStyles.None, out curDate))
                                        DateTime.TryParse(CellDate.GetString(), new CultureInfo("en-US"), DateTimeStyles.None, out curDate);

                                }
                            }
                            string phoneNumber = CellDate.CellBelow().GetValue<string>();
                            var CellPhoneNumber = CellDate.CellBelow();
                            if (phoneNumber == "")
                            {
                                phoneNumber = CellDate.CellBelow().CellBelow().GetValue<string>();
                                CellPhoneNumber = CellPhoneNumber.CellBelow();
                            }

                            string link;
                            if (CellPhoneNumber.HasHyperlink)
                                link = CellPhoneNumber.GetHyperlink().ExternalAddress.AbsoluteUri;
                            else
                                link = "";

                            if (phoneNumber != "")
                            {
                                curCall.ClientName = phoneNumber;
                                curCall.ClientLink = link;
                                curCall.ManagerID = idManager;
                                curCall.Date = curDate;

                                try
                                {
                                    curCall.StageID = db.Stages.Where(s => Regex.Match(s.Name, page.Name.Trim(), RegexOptions.IgnoreCase).Success).First().Id;
                                }
                                catch (InvalidOperationException)
                                {
                                    return "В системе отсутствует этап " + page.Name;
                                }
                                curCall.Correction = page.Cell(corrRow, CellDate.Address.ColumnNumber).GetString();
                                curCall.Date = curDate;
                                TimeSpan duration;
                                IXLCell CellPoint = CellDate.CellBelow().CellBelow().CellBelow();
                                if (CellPoint.DataType == XLDataType.DateTime)
                                    CellPoint.DataType = XLDataType.TimeSpan;
                                TimeSpan.TryParse(CellPoint.GetString(), out duration);
                                XLColor CorrColor = page.Cell(corrRow, CellDate.Address.ColumnNumber).Style.Fill.BackgroundColor;
                                if (CorrColor == XLColor.Red)
                                {
                                    curCall.correctioncolor = "red";
                                }
                                else
                                {
                                    if (CorrColor == XLColor.Lime)
                                    {
                                        curCall.correctioncolor = "green";
                                    }
                                    else
                                    {
                                        curCall.correctioncolor = "no color";
                                    }
                                }
                                IXLCell CellNamePoint;
                                int markOfPoint;
                                TimeSpan wrongtime1 = new TimeSpan(1, 0, 0, 0);
                                TimeSpan wrongtime2 = new TimeSpan();
                                curCall.Points = new List<DBModels.Point>();
                                DBModels.Point dbpoint;
                                if (wrongtime1 <= duration || duration == wrongtime2)
                                {
                                    duration = wrongtime2;
                                    if (CellPoint.TryGetValue<int>(out markOfPoint))
                                    {
                                        dbpoint = new DBModels.Point();
                                        CellNamePoint = page.Cell(CellPoint.Address.RowNumber, numColPoint);
                                        dbpoint.Value = markOfPoint;
                                        dbpoint.red = CellPoint.Style.Fill.BackgroundColor == XLColor.Red;

                                        int num = 0;
                                        try
                                        {
                                            if (!page.Cell("C" + (CellPoint.Address.RowNumber).ToString()).TryGetValue<int>(out num))
                                                throw new InvalidOperationException();
                                            dbpoint.AbstractPointID = db.AbstractPoints.Where(p => CellNamePoint.GetString() == p.name && p.num == num).First().Id;
                                        }
                                        catch (InvalidOperationException)
                                        {
                                            return "В системе из этапа " + page.Name + " в файле " + file.Name + " отсутствует пункт " + num.ToString() + ". " + CellNamePoint.GetString();
                                        }

                                        curCall.Points.Add(dbpoint);
                                    }
                                }
                                curCall.duration = duration;
                                CellPoint = CellDate.CellBelow().CellBelow().CellBelow().CellBelow();
                                dbpoint = new DBModels.Point();
                                if (!CellPoint.TryGetValue<int>(out markOfPoint))
                                {
                                    if (CellPoint.GetString() != "")
                                    {
                                        curCall.DealName = CellPoint.GetString();

                                    }
                                }
                                else
                                {
                                    CellNamePoint = page.Cell(CellPoint.Address.RowNumber, numColPoint);
                                    dbpoint.Value = markOfPoint;
                                    dbpoint.red = CellPoint.Style.Fill.BackgroundColor == XLColor.Red;
                                    int num = 0;
                                    try
                                    {
                                        if (!page.Cell("C" + (CellPoint.Address.RowNumber).ToString()).TryGetValue<int>(out num))
                                            throw new InvalidOperationException();
                                        dbpoint.AbstractPointID = db.AbstractPoints.Where(p => CellNamePoint.GetString() == p.name && p.num == num).First().Id;
                                    }
                                    catch (InvalidOperationException)
                                    {
                                        return "В системе из этапа " + page.Name + " в файле " + file.Name + " отсутствует пункт " + num.ToString() + ". " + CellNamePoint.GetString();
                                    }

                                    curCall.Points.Add(dbpoint);
                                }
                                CellPoint = CellPoint.CellBelow();
                                while (CellPoint.Address.RowNumber < corrRow - 4)
                                {
                                    dbpoint = new DBModels.Point();
                                    if (CellPoint.TryGetValue<int>(out markOfPoint))
                                    {
                                        CellNamePoint = page.Cell(CellPoint.Address.RowNumber, numColPoint);
                                        dbpoint.Value = markOfPoint;
                                        dbpoint.red = CellPoint.Style.Fill.BackgroundColor == XLColor.Red;
                                        int num = 0;
                                        try
                                        {
                                            if (!page.Cell("C" + (CellPoint.Address.RowNumber).ToString()).TryGetValue<int>(out num))
                                                throw new InvalidOperationException();
                                            dbpoint.AbstractPointID = db.AbstractPoints.Where(p => CellNamePoint.GetString() == p.name && p.num == num).First().Id;
                                        }
                                        catch (InvalidOperationException)
                                        {
                                            return "В системе из этапа " + page.Name + " в файле " + file.Name + " отсутствует пункт " + num.ToString() + ". " + CellNamePoint.GetString();

                                        }

                                        curCall.Points.Add(dbpoint);
                                    }
                                    CellPoint = CellPoint.CellBelow();
                                }


                                if (Regex.Match(page.Cell("A" + (corrRow + 6).ToString()).GetString().ToLower().Trim(), "дата", RegexOptions.IgnoreCase).Success)
                                {
                                    var NextContactCell = page.Cell(corrRow + 6, CellDate.Address.ColumnNumber);
                                    if (NextContactCell.GetString() != "")
                                    {
                                        var DateNext = new DateTime();
                                        if (NextContactCell.DataType == XLDataType.DateTime)
                                            DateNext = NextContactCell.GetDateTime();
                                        else
                                        {
                                            if (!DateTime.TryParse(NextContactCell.GetString(), new CultureInfo("ru-RU"), DateTimeStyles.None, out DateNext))
                                            {
                                                if (DateTime.TryParse(NextContactCell.GetString(), new CultureInfo("en-US"), DateTimeStyles.None, out DateNext)) ;
                                            }

                                        }
                                        if (DateNext.Year > 2000)
                                        {
                                            curCall.DateNext = DateNext;
                                        }
                                    }
                                    if (Regex.Match(page.Cell(corrRow + 5, CellDate.Address.ColumnNumber).GetString(), "работ", RegexOptions.IgnoreCase).Success)
                                    {
                                        curCall.ClientState = "Work";
                                    };
                                    if (Regex.Match(page.Cell(corrRow + 5, CellDate.Address.ColumnNumber).GetString(), "упущ", RegexOptions.IgnoreCase).Success)
                                    {
                                        curCall.ClientState = "Missed";
                                        curCall.DateOfClose = curDate;
                                    };
                                    if (Regex.Match(page.Cell(corrRow + 5, CellDate.Address.ColumnNumber).GetString(), "успеш", RegexOptions.IgnoreCase).Success)
                                    {
                                        curCall.ClientState = "Success";
                                        curCall.DateOfClose = curDate;
                                    };
                                }

                                curCall.DateCreate = DateTime.Now;
                                try
                                {
                                    if (curCall.Points.Count() > 0)
                                    {
                                        db.Calls.Add(curCall);
                                        db.SaveChanges();
                                    }
                                }
                                catch (DbUpdateException e)
                                {
                                    return "Невозможно сохранить звонок" + curCall.ClientName + " из этапа " + page.Name + " файла" + file.Name;
                                }

                            }
                            
                            CellDate = CellDate.CellRight();
                        }





                    }
                    
            }
            return "";
        }
    }
}
