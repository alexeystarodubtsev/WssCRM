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
        public List<Manager> managers = new List<Manager>();
        public Company(string name, int id)
        {
            Name = name;
            this.id = id;
        }
        public Company()
        {

        }
    }
}
