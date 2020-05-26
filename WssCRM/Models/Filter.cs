using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class Filter
    {
        public List <Company> Companies { get; set; }
        public Filter()
        {
            Companies = new List<Company>();
        }
    }
}
