using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class Stage
    {
        public string name;
        public int id;
        public List<Point> points = new List<Point>();
    }
}
