using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WssCRM.Models;
namespace WssCRM
{
    public class Call
    {
        public string Stage { get; set; }
        public string Company { get; set; }
        public string Date { get; set; }
        public List <Point> points { get; set; }
        public int id { get; set; }
        public Call ()
        {
            points = new List<Point>();
        }

    }
}
