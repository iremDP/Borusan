using BorusanServices.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorusanServices.Models.Dto
{
    public class DtoSiparis
    {

        public string MusteriSiparisNo { get; set; }

        public string CikisAdresi { get; set; }

        public string VarisAdresi { get; set; }

        public int Miktar { get; set; }

        public MiktarBirims MiktarBirim { get; set; }

        public decimal Agirlik { get; set; }

        public AgirlikBirims AgirlikBirim { get; set; }


        public string MalzemeKodu { get; set; }

        public string MalzemeAdi { get; set; }

        public string Not { get; set; }

        public SiparisDurums SiparisDurum { get; set; }

        public DateTime DegisimTarihi { get; set; }

    }
}
