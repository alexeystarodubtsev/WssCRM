using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class RowInTableStatistic
    {
        public string rowname;
        public List<CellInStatistic> Cells = new List<CellInStatistic>();
        public RowInTableStatistic(string manager)
        {
            rowname = manager;
            Cells = new List<CellInStatistic>();
        }
    }
}
