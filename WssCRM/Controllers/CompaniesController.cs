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
    public class CompaniesController : ControllerBase
    {
        private DBModels.ApplicationContext db;
        public CompaniesController(DBModels.ApplicationContext context)
        {
            db = context;
        }
        [HttpGet]
        public IEnumerable<Company> GetCompanies()
        {
            List<Company> Companies = new List<Company>();
            foreach (var comp in db.Companies)
            {
                Companies.Add(new Company(comp.name,comp.Id));
            }
            return Companies;
        }

        
        [HttpPost("processcalls")]
        public IActionResult ProcessCalls()
        {
            int CompanyID;
            if (int.TryParse(Request.Form[Request.Form.Keys.First()], out CompanyID))
            {
                string ans = new ProcessingCall(db).ProcessingCalls(CompanyID, Request.Form.Files);
                if (ans == "")
                    return Ok();
                else
                {
                    ModelState.AddModelError("", ans);
                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError("badCompanyID", "Неверный идентификатор компании, обратитесь к разработчикам");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("processfile")]
        public Company ProcessCompany()
        {
            return new ProcessingCompany(db).ParsingXLS(Request.Form.Files[0]);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteCompany (int id)
        {
            try
            {
                db.Companies.Remove(db.Companies.Where(c => c.Id == id).First());
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("can'tdel", "Не удалось удалить");
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpGet("{id}")]
        public Company GetCompany(int id)
        {
            return new ProcessingCompany(db).GetCompany(id);
        }
        [HttpPost]
        public IActionResult AddCompany(Company clientComp)
        {
            if (ModelState.IsValid)
            {
                var ans = new ProcessingCompany(db).AddCompany(clientComp);
                if (ans == "")
                    return Ok(clientComp);
                else
                {
                    ModelState.AddModelError("key", ans);
                    return BadRequest(ModelState);
                }
            }
            ModelState.AddModelError("other", "Что-то пошло не так, обратитесь к разработчикам");
            return BadRequest(ModelState);
        }
    }
}