using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WssCRM.Models;
using WssCRM.Processing;

namespace WssCRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallsController : ControllerBase
    {
        private DBModels.ApplicationContext db;
        public CallsController(DBModels.ApplicationContext context)
        {
            db = context;
        }
        [HttpPost]
        public PartialCalls GetCalls(ChooseFilter f1)
        {

            return new ProcessingCall(db).GetCalls(f1);
        }

        // GET api/newcall/companyID/StageID
        [HttpPost("getnewcall")]
        public Call GetNewCall(Call call)
        {
           
            return new ProcessingCall(db).NewCall(call.Company.id, call.Stages.Select(s => s.id).ToList());
        }

        [HttpGet("{id}")]
        public Call GetCall(int id)
        {
            return new ProcessingCall(db).GetCall(id);
            
        }
        
        [HttpPost("getstatistics")]
        public ICollection<StatisticStage> getStatistics(ChooseFilter f1)
        {
            return new Statistics(db).getStatistics(f1);

        }
        [HttpGet("Flt/{opt}")]
        public Filter GetFilter(string opt)
        {

            return new ProcessingCall(db).getFilter(opt);

        }
        [HttpPost("newcall")]
        public IActionResult AddCall(Call call)
        {
            string err = new ProcessingCall(db).AddNewCall(call);
            if (err !="")
            {
                return BadRequest(err);
            }
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateCall(Call call)
        {
            string err = new ProcessingCall(db).UpdateCall(call);
            if (err != "")
            {
                return BadRequest(err);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            try
            {
                db.Calls.Remove(db.Calls.Where(c => c.Id == id).First());
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("can'tdel", "Не удалось удалить");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpPost ("getmissed")]
        public PartialMissedCalls getMissed(ChooseFilter f1)
        {
            return new AnalyzeCalls(db).getMissedCalls(f1);
        }

        [HttpPut("passedon")]
        public IActionResult passOnToCustomer(MissedCall c)
        {
            string ans = new AnalyzeCalls(db).passOnToCustomer(c);
            if (ans == "")
                return Ok();
            else
            {
                ModelState.AddModelError("other", ans);
                return BadRequest(ModelState);
            }
        }

    }

}