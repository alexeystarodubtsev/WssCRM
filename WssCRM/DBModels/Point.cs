using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.DBModels
{
    public class Point
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CallID { get; set; }
        public int Value { get; set; }
        public int AbstractPointID { get; set; }
        //public AbstractPoint PointData {get; set; }
    }
}
