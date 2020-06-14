using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Name { get; set; }
        public bool deleted { get; set; }
        public bool agreementStage { get; set; }
        public bool preAgreementStage { get; set; }
        public bool incomeStage { get; set; }
        public int Num { get; set; }
        public ICollection <AbstractPoint> Points { get; set; }
        public ICollection<Call> Calls { get; set; }
    }
}
