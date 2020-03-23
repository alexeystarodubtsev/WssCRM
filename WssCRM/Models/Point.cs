using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.Models
{
    public class Point
    {
        public string Name { get; set; }
        public bool Value { get; set; }
        public Point(string name, bool val)
        {
            Name = name;
            Value = val;
        }
    }
}
