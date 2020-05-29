using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WssCRM.Models;
namespace WssCRM
{
    public class Call
    {
        public Stage Stage { get; set; }
        public Company Company { get; set; }
        public DateTime Date { get; set; }
        public string comment { get; set; }
        public TimeSpan duration { get; set; }
        public Manager manager { get; set; }
        public List <Point> points { get; set; }
        public int id { get; set; }
        public string ClientName { get; set; }
        public string ClientLink { get; set; }
        public Call ()
        {
            points = new List<Point>();
        }

    }
}
