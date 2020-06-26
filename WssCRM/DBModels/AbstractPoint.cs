using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.DBModels
{
    public class AbstractPoint
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int maxMark { get; set; }
        public int num { get; set; }
        [Required]
        public string name { get; set; }
        public int StageID { get; set; }
        public bool deleted { get; set; }
    }
}
