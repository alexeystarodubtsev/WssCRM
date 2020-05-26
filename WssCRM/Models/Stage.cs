﻿using System;
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
        public Stage()
        {

        }
        public Stage(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}
