using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WssCRM.Models;
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
        [HttpGet("{id}")]
        public Call GetCall(int id)
        {

            Call call = new Call();
            call.Company = "company";
            call.Date = DateTime.Now.ToString("yyyy-MM-dd");
            call.Stage = "first";
            call.id = 2;
            call.points.Add(new Point("Приветствие"));
            call.points.Add(new Point("Попрощался"));

            return call;
        }
        [HttpGet]
        public Filter GetFilter()
        {
            
            Filter F1 = new Filter();
            ChooseFilter posFilter;
            foreach(var dbcomp in db.Companies)
            {
                posFilter = new ChooseFilter(new Company(dbcomp.name, dbcomp.Id));
                F1.pointsFilter.Add(posFilter);
            }
            
            return F1;
        }
    }

}