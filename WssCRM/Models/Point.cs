using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class Point
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int maxMark { get; set; }
        public Point(string name, int val)
        {
            Name = name;
            Value = val;
        }
        public Point(string name, int id, int maxMark)
        {
            Name = name;
            this.maxMark = maxMark;
        }
        public Point(string name)
        {
            Name = name;
            
        }
        public Point()
        {

        }
    }
}
