using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class TableWithStatistic
    {
        public string TableName;
        public List<string> Period = new List<string>();
        public HashSet<string> Managers = new HashSet<string>();
        public List <RowInTableStatistic> Data = new List<RowInTableStatistic>(); //Менеджер, период, значение
        public string Mode;       
        
        public TableWithStatistic(string name)
        {
            TableName = name;
            Data = new List<RowInTableStatistic>();
        }

    }
}
