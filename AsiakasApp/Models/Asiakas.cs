using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AsiakasApp.Models
{
    public class Asiakas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Avain { get; set; }
        public string Nimi { get; set; }
        public string Osoite { get; set; }
        public string Postinro { get; set; }
        public string Postitmp { get; set; }
        public DateTime? Luontipvm { get; set; }
        public int? AsiakastyyppiID { get; set; }

        public Asiakastyyppi Asiakastyyppi { get; set; }
    }
}
