using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AsiakasApp.Models
{
    public class Asiakastyyppi
    {
        /*
        [ForeignKey("Asiakas")]
        [Key]
        */
        public int AsiakastyyppiID { get; set; }
        public string Lyhenne { get; set; }
        public string Selite { get; set; }

        IEnumerable<Asiakas> Asiakkaat { get; set; }
    }
}
