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
        public bool deleted { get; set; }
        public List<Point> points = new List<Point>();
        public Stage()
        {

        }
        public Stage(string name, int id, bool deleted)
        {
            this.name = name;
            this.id = id;
            this.deleted = deleted;
        }
    }
}
