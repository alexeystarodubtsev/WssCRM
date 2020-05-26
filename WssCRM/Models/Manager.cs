using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class Manager
    {
        public string name { get; set; }
        public int id { get; set; }
        public Manager(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}
