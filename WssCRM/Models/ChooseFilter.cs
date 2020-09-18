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
        public int pageNumber { get; set; }
        public bool onlyNotProcessed { get; set; }
        public string period { get; set; }
        public ChooseFilter(Company company, Stage stage, Manager manager, DateTime StartDate, DateTime EndDate, int pageNumber, bool onlyNotProcessed = true, string period = "month")
        {
            this.Company = company;
            this.stage = stage;
            this.manager = manager;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.pageNumber = pageNumber;
            this.onlyNotProcessed = onlyNotProcessed;
            this.period = period;
        }
        public ChooseFilter()
        {

        }
    }
}
