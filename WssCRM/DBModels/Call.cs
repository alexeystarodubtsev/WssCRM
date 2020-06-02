using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WssCRM.DBModels
{
    public class Call
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ClientName { get; set; }
        public string ClientLink { get; set; }
        //public int CompanyID { get; set; }
        public int StageID { get; set; }
        [Required]
        public string ClientState { get; set; }
        [Required]
        public string Correction { get; set; }
        [Required]
        public TimeSpan duration { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int ManagerID { get; set; }
        public string correctioncolor { get; set; }
        public System.Nullable<DateTime> DateNext { get; set; }
        public System.Nullable<DateTime> DateOfClose { get; set; }
        public ICollection<Point> Points { get; set; }
        
        
    }
}
