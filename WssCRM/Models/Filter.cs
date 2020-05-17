using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class Filter
    {
        public List <ChooseFilter> pointsFilter { get; set; }
        public Filter()
        {
            pointsFilter = new List<ChooseFilter>();
        }
    }
}
