using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENL___WarehouseManagementSystem.Data.DataModels
{

    [Table("Produkt")]
    public class Produkt
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProdID { get; set; }

        public string ProdNavn { get; set; }

        public int ProdAntal { get; set; }

        public string ProdBeskrivelse { get; set; }

        public string ProdPlacering { get; set; }

        public DateTime Oprettelse { get; set; }

    }
}
