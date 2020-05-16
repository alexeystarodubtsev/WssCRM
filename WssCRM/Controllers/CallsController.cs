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
        [HttpPost]
        public IEnumerable<Call> GetCalls(ChooseFilter f1)
        {
            List<Call> l = new List<Call>();
            Call call = new Call();
            call.Company = "company";
            call.Date = DateTime.Now.ToString("yyyy-MM-dd");
            call.Stage = "first";
            call.id = 3;
            if (call.Company == f1.company)
            {
                l.Add(call);
            }
            call = new Call();
            call.Company = "company2";
            call.Date = DateTime.Now.ToString("yyyy-MM-dd");
            call.Stage = "second";
            call.id = 4;
            if (call.Company == f1.company)
            {
                l.Add(call);
            }
            call = new Call();
            call.Company = "company2";
            call.Date = DateTime.Now.ToString("yyyy-MM-dd");
            call.Stage = "third";
            call.id = 4;
            if (call.Company == f1.company)
            {
                l.Add(call);
            }
            call = new Call();
            call.Company = "company";
            call.Date = DateTime.Now.ToString("yyyy-MM-dd");
            call.Stage = "for";
            call.id = 4;
            if (call.Company == f1.company)
            {
                l.Add(call);
            }
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
            F1.companies.Add("company");
            F1.companies.Add("company2");
            F1.companies.Add("Все компании");
            return F1;
        }
    }

}