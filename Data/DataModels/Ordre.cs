using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ENL___WarehouseManagementSystem.Data.DataModels
{
    [Table("Ordre")]
    public class Ordre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdreID { get; set; }
        public string ProdNavn { get; set; }
        public int OrdreAntal { get; set; }
        public string OrdreStatus { get; set; }
        public string EmpName { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("EmpName")]
        public virtual Employee Employee { get; set; }
    }
}
