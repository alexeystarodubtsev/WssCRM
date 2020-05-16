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
    public class CompaniesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Company> GetCompanies()
        {
            List<Company> Companies = new List<Company>();
            Companies.Add(new Company("Company"));
            return Companies;
        }
        [HttpGet("{id}")]
        public Company GetCompany(int id)
        {
            return new Company("Первая компания");
        }
        [HttpPost]
        public IActionResult AddCompany(Company c)
        {
            if (ModelState.IsValid)
            {
                return Ok(c);
            }
            return BadRequest(ModelState);
        }
    }
}