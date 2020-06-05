using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class PartialCalls
    {
        public List<Call> calls = new List<Call>();
        public int pageSize { get; set; }
    }
}
