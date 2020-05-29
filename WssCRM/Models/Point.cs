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
        public Point(string name, int value, int maxMark, int idAbstract)
        {
            Name = name;
            this.maxMark = maxMark;
            this.Value = value;
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
