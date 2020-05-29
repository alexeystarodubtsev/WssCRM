using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Call> GetCalls(ChooseFilter f1)
        {
            
            List<Call> l = new List<Call>();
            
            return l;
        }

        // GET api/newcall/companyID/StageID
        [HttpGet("newcall/{companyID}/{StageID}")]
        public Call GetNewCall(int companyID, int StageID)
        {

            return new ProcessingCall(db).NewCall(companyID,StageID);
        }

        [HttpGet("{id}")]
        public Call GetCall(int id)
        {
            return new ProcessingCall(db).GetCall(id);
            
        }
        [HttpGet("Flt/{opt}")]
        public Filter GetFilter(string opt)
        {

            return new ProcessingCall(db).getFilter(opt);

        }
    }

}