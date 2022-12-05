using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BorusanServices.Models.Entity
{
    public class Malzeme
    {
        [Key]
        public string MalzemeKodu { get; set; }

        [Required]
        public string MalzemeAdi { get; set; }

        public List<Siparis> Siparis { get; set; }
    }
}
