using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class MissedCall
    {
        public int id { get; set; }
        public string ClientName { get; set; }
        public string ClientLink { get; set; }
        public string clientState { get; set; }
        public string date { get; set; }
        public string Reason { get; set; }
        public string correction { get; set; }
        public string NoticeCRM { get; set; }
        public string DateNext { get; set; }
        public string Manager { get; set; }
        public bool   processed { get; set; }


    }
}
