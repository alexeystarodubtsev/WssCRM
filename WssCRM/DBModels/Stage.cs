using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.DBModels
{
    public class Stage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public ICollection <AbstractPoint> Points { get; set; }
    }
}
