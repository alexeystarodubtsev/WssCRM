using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class Company
    {
        public string Name { get; set; }
        public int id { get; set; }
        public List<Stage> stages = new List<Stage>();
        public List<string> managers = new List<string>();
        public Company(string name)
        {
            Name = name;
            this.id = 1;
        }
    }
}
