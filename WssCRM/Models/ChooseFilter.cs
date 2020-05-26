using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class ChooseFilter
    {
        public Company Company { get; set; }
        public Stage stage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Manager manager { get; set; }
        public ChooseFilter(Company company, Stage stage, Manager manager, DateTime StartDate, DateTime EndDate)
        {
            this.Company = company;
            this.stage = stage;
            this.manager = manager;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
}
