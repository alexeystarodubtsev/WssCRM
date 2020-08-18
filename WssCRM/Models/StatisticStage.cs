using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class StatisticStage
    {
        public string StageName;
        public List<TableWithStatistic> Tables = new List<TableWithStatistic>();
        public StatisticStage(string name)
        {
            StageName = name;
            Tables = new List<TableWithStatistic>();
        }
    }
}
