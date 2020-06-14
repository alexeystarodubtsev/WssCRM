using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class PartialMissedCalls
    {
        public List<MissedCall> calls = new List<MissedCall>();
        public int pageSize { get; set; }
    }
}
