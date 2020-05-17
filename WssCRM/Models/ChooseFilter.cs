using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class ChooseFilter
    {
        public Company Company { get; set; }
        public ChooseFilter(Company company)
        {
            this.Company = company;
        }
    }
}
