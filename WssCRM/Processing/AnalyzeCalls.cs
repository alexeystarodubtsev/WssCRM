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
            var query = from c in db.Calls.Where(c => stagesid.Contains(c.StageID) && c.ClientLink != null && c.ClientLink != "")
                        from man in db.Managers.Where(m => m.Id == c.ManagerID)
                        from s in db.Stages.Where(s => s.Id == c.StageID)
                        group c by new { c.ClientName, c.ClientLink } into g
                        from c2 in db.Calls.Where(c => c.ClientName == g.Key.ClientName
                                && g.Key.ClientLink == c.ClientLink
                                && c.Date == g.Max(o => o.Date)
                                && stagesid.Contains(c.StageID)
                                && (c.ClientState == "Work" && (!c.DateNext.HasValue || c.DateNext.Value > DateTime.Today) 
                                      || c.ClientState == "Missed"
                                   )
                                )
                                .Take(1)
                        from s2 in db.Stages.Where(s => s.Id == c2.StageID && (s.preAgreementStage || s.incomeStage || DateTime.Today.Subtract(c2.Date).TotalDays > shiftDays))

                        select new { Reason = ("" + (s2.incomeStage ? "Входящий, на который не перезвонили" : "")
                                   + (DateTime.Today.Subtract(c2.Date).TotalDays > shiftDays ?
                                            "\nПоследний звонок: "
                                                + (int)DateTime.Today.Subtract(c2.Date).TotalDays / 7 + " недел"
                                                + (((int)DateTime.Today.Subtract(c2.Date).TotalDays / 7 < 5) ? "и" : "ь")
                                                + " назад"
                                            : "")
                                   + (s2.preAgreementStage ? "\nНе был закрыт в договор с предшествующего этапа" : "")).Trim('\n')

                        
                            , g.Key, Date = g.Max(o => o.Date), c2.Correction, c2.DateNext, c2.ClientState, c2.Id}
                        ;
           
            
            PartialMissedCalls PartCalls = new PartialMissedCalls();
            int qty = 1;
            int qtyonsamepage = 20;
            PartCalls.pageSize = (qty / qtyonsamepage) + ((qty % qtyonsamepage) > 0 ? 1 : 0);
            foreach (var call in query)
            {

                
                var msCall = new MissedCall();
                msCall.ClientName = call.Key.ClientName;
                msCall.ClientLink = call.Key.ClientLink;
                //msCall.date = call.Date.ToString("dd.MM.yyyy");
                msCall.Reason = call.Reason;
                msCall.NoticeCRM = "Пробуем";
                msCall.correction = call.Correction;
                msCall.date = call.Date.ToString("dd.MM.yyyy");
                if (call.DateNext.HasValue)
                    msCall.DateNext = call.DateNext.Value.ToString("dd.MM.yyyy");
                msCall.clientState = call.ClientState; 

                PartCalls.calls.Add(msCall);
            }
            return PartCalls;
        }
    }
}
