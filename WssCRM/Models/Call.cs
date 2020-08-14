using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WssCRM.Models;
namespace WssCRM
{
    public class Call
    {
        public List<Stage> Stages { get; set; }
        public Company Company { get; set; }
        public DateTime Date { get; set; }
        public string Correction { get; set; }
        public TimeSpan duration { get; set; }
        public Manager manager { get; set; }
        //public List <Point> points { get; set; }
        public int id { get; set; }
        public string ClientName { get; set; }
        public string ClientLink { get; set; }
        public string correctioncolor { get; set; }
        public string clientState { get; set; }
        public DateTime dateNext { get; set; }
        public bool hasObjections { get; set; }
        public bool hasDateNext { get; set; }
        public bool firstCalltoClient { get; set; }
        public List<Objection> Objections { get; set; }
        public Call ()
        {
            //points = new List<Point>();
            duration = new TimeSpan();
            Objections = new List<Objection>();
            Stages = new List<Stage>();
        }

    }
}
