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
        [HttpGet]
        public IEnumerable<Call> Get()
        {
            List<Call> l = new List<Call>();
            Call call = new Call();
            call.Company = "company";
            call.Date = DateTime.Now.ToString("yyyy-MM-dd");
            call.Stage = "first";
            call.id = 3;
            l.Add(call);
            return l;
        }
        [HttpGet("{id}")]
        public Call Get(int id)
        {

            Call call = new Call();
            call.Company = "company";
            call.Date = DateTime.Now.ToString("yyyy-MM-dd");
            call.Stage = "first";
            call.id = 2;
            call.points.Add(new Point("Приветствие", false));
            call.points.Add(new Point("Попращался", true));

            return call;
        }
    }

}