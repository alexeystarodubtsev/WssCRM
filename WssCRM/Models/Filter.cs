using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class Filter
    {
        public List <string> companies { get; set; }
        public Filter()
        {
            companies = new List<string>();
        }
    }
}
