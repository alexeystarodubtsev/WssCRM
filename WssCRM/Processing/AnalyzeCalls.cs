using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WssCRM.Models;

namespace WssCRM.Processing
{
    public class AnalyzeCalls
    {
        DBModels.ApplicationContext db;
        public AnalyzeCalls(DBModels.ApplicationContext context)
        {
            db = context;
        }
        public PartialMissedCalls getMissedCalls(ChooseFilter f1)
        {
            int shiftDays = db.Companies.Where(c => c.Id == f1.Company.id).Select(c => c.daysForAnalyze).First() + 2;
            List<int> stagesid = db.Stages.Where(s => f1.Company.id == s.CompanyID).Select(s => s.Id).ToList<int>();
            var query = from c in db.Calls.Where(c => stagesid.Contains(c.StageID) && c.ClientLink != null && c.ClientLink != ""
                                && (c.Date >= db.Calls.Where(cc => cc.ClientName == c.ClientName
                                        && c.ClientLink == cc.ClientLink
                                        && stagesid.Contains(cc.StageID)
                                        && cc.firstCalltoClient == true
                                        ).OrderByDescending(cc => cc.Date).FirstOrDefault().Date
                                || db.Calls.Where(cc => cc.ClientName == c.ClientName
                                        && c.ClientLink == cc.ClientLink
                                        && stagesid.Contains(cc.StageID)
                                        && cc.firstCalltoClient == true
                                        ).OrderByDescending(cc => cc.Date).FirstOrDefault().Date ==null)
                                )
                        //from firstDate in db.Calls.Where(cc => cc.ClientName == c4.ClientName
                        //       && c4.ClientLink == cc.ClientLink
                        //       && stagesid.Contains(cc.StageID)
                        //       && cc.firstCalltoClient == true
                        //        ).OrderByDescending(cc => cc.Date).Take(1).Select(cc => cc.Date)
                        //from c in db.Calls.Where(cc => cc.Id == c4.Id
                        //        //&& (cc.Date >= firstDate || firstDate == null)
                        //        )
                        from man in db.Managers.Where(m => m.Id == c.ManagerID).Select(m => m.name)
                        from s in db.Stages.Where(s => s.Id == c.StageID)
                        group new { c, man, s } by new { c.ClientName, c.ClientLink } into g
                        //from c2 in db.Calls.Where(c => c.ClientName == g.Key.ClientName
                        //        && g.Key.ClientLink == c.ClientLink
                        //        && c.Date == g.Max(o => o.c.Date)
                        //        && stagesid.Contains(c.StageID)
                        //        )
                        from maxStage in db.Stages.Where(s => s.CompanyID == f1.Company.id && s.Num == g.Max(o => o.s.Num)).Take(1)
                        from firstDateonthisStage in db.Calls.Where(c => 
                                c.ClientLink == g.Key.ClientLink
                                && c.ClientName == g.Key.ClientName
                                && g.Any(l => l.c.Id == c.Id) 
                                && c.StageID == maxStage.Id
                                
                                ).OrderBy(c => c.Date).Take(1).Select(c => c.Date)
                        from s2 in db.Stages.Where(s => s.Id == db.Calls.Where(c => c.ClientName == g.Key.ClientName
                                && g.Key.ClientLink == c.ClientLink
                                && c.Date == g.Max(o => o.c.Date)
                                && stagesid.Contains(c.StageID)
                                ).OrderByDescending(cc => db.Stages.Where(ss => ss.Id == cc.StageID).First().Num).First().StageID)
                        .Take(1)
                        from c2 in db.Calls.Where(c => c.ClientName == g.Key.ClientName
                                && g.Key.ClientLink == c.ClientLink
                                && c.Date == g.Max(o => o.c.Date)
                                && c.StageID == s2.Id
                                ).Take(1)
                        .Where(cc => 
                            (maxStage.preAgreementStage||
                             s2.incomeStage
                             || (DateTime.Today.Subtract(cc.Date).TotalDays > shiftDays) 
                             || db.Calls.Where(ccc => ccc.ClientName == cc.ClientName && ccc.ClientLink == cc.ClientLink && ccc.Date >= firstDateonthisStage && stagesid.Contains(ccc.StageID)).Select(ccc=> ccc.Date).Distinct().Count() >= 2
                             )
                             && (cc.ClientState == "Work" && (!cc.DateNext.HasValue || cc.DateNext.Value < DateTime.Today)
                                || cc.ClientState == "Missed"
                             )
                             && (cc.passedOnToCustomer == !f1.onlyNotProcessed || !f1.onlyNotProcessed))
                        

                        select new { Reason = ("" + (s2.incomeStage ? "Входящий, на который не перезвонили" : "")
                                   + (DateTime.Today.Subtract(c2.Date).TotalDays > shiftDays ?
                                            ",\nПоследний звонок: "
                                                + (int)DateTime.Today.Subtract(c2.Date).TotalDays / 7 + " недел"
                                                + (((int)DateTime.Today.Subtract(c2.Date).TotalDays / 7 < 5) ? "и" : "ь")
                                                + " назад"
                                            : "")
                                   + (db.Calls.Where(ccc => ccc.ClientName == c2.ClientName && ccc.ClientLink == c2.ClientLink && ccc.Date >= firstDateonthisStage && stagesid.Contains(ccc.StageID)).Select(ccc => ccc.Date).Distinct().Count() >= 2 ? 
                                   ",\nЗастрял на этапе: " + maxStage.Name + " - " 
                                   + db.Calls.Where(ccc => ccc.ClientName == c2.ClientName && ccc.ClientLink == c2.ClientLink && ccc.Date >= firstDateonthisStage && stagesid.Contains(ccc.StageID)).Count().ToString()
                                   + "шт. от " + String.Join(", ", (db.Calls.Where(ccc => ccc.ClientName == c2.ClientName && ccc.ClientLink == c2.ClientLink 
                                                    && ccc.Date >= firstDateonthisStage && stagesid.Contains(ccc.StageID)).Select(ccc => ccc.Date).OrderBy(date => date).Distinct()
                                                    .Select(date=>date.ToString("dd.MM.yyyy")).ToArray()))
                                   : "")
                                   + (maxStage.preAgreementStage ? ",\nНе был закрыт в договор с предшествующего этапа" : "")).Trim(',').Trim('\n')


                            , g.Key,
                            Date = g.Max(o => o.c.Date),
                            c2.Correction,
                            c2.DateNext,
                            c2.ClientState,
                            c2.Id,
                            c2.passedOnToCustomer,
                            c2.DateOfClose,
                            DATA = g.AsEnumerable().ToList()
                        }
                        ;
           
            
            PartialMissedCalls PartCalls = new PartialMissedCalls();
            int qty = query.Count();
            int qtyonsamepage = 20;
            foreach (var call in query.Skip((f1.pageNumber - 1) * qtyonsamepage).Take(qtyonsamepage))
            {


                var msCall = new MissedCall();
                msCall.ClientName = call.Key.ClientName;
                msCall.ClientLink = call.Key.ClientLink;
                //msCall.date = call.Date.ToString("dd.MM.yyyy");
                msCall.Reason = call.Reason;
                msCall.NoticeCRM = "Пробуем";
                msCall.correction = call.Correction;
                msCall.date = call.Date.ToString("dd.MM.yyyy");
                if (call.DateNext.HasValue && call.ClientState == "Work")
                    msCall.DateNext = call.DateNext.Value.ToString("dd.MM.yyyy");
                if (call.ClientState == "Missed" && call.DateOfClose.HasValue)
                    msCall.DateNext = call.DateOfClose.Value.ToString("dd.MM.yyyy");
                HashSet<string> Managers = new HashSet<string>();
                foreach (var o in call.DATA)
                {
                    if (!Managers.Contains(o.man))
                        Managers.Add(o.man);
                }
                msCall.Manager = string.Join(", ", Managers.ToArray());
                msCall.clientState = call.ClientState == "Missed" ? "Упущен" : "В работе";
                msCall.processed = call.passedOnToCustomer;
                msCall.id = call.Id;
                PartCalls.calls.Add(msCall);
            }

            PartCalls.pageSize = (qty / qtyonsamepage) + ((qty % qtyonsamepage) > 0 ? 1 : 0);
            return PartCalls;
        }
        public string passOnToCustomer(MissedCall missedcall)
        {
            var dbcall = db.Calls.Where(c => c.Id == missedcall.id).First();
            dbcall.passedOnToCustomer = missedcall.processed;
            db.Calls.Update(dbcall);
            db.SaveChanges();
            return "";
        }
    }
}
