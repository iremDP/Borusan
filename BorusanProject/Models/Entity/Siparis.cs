using BorusanServices.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BorusanServices.Models.Entity
{
    public class Siparis
    {
        [Key]
        public int SistemSiparisNo { get; set; }

        [Required]
        public string MusteriSiparisNo { get; set; }

        [Required]
        public string CikisAdresi { get; set; }

        [Required]
        public string VarisAdresi { get; set; }

        [Required]
        public int Miktar { get; set; }

        [Required]
        public MiktarBirims MiktarBirim { get; set; }

        [Required]
        public decimal Agirlik { get; set; }

        [Required]
        public AgirlikBirims AgirlikBirim { get; set; }

        [Required]
        [ForeignKey("Malzeme")]
        public string MalzemeKodu { get; set; }

        public Malzeme Malzeme { get; set; }

        public string Not { get; set; }

        [Required]
        public SiparisDurums SiparisDurum { get; set; }

        [Required]
        public DateTime DegisimTarihi { get; set; }


    }
}
