using BorusanServices.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorusanServices.Models.Dto
{
    public class DtoSiparisCevap: DtoSiparis
    {
        public int SistemSiparisNo { get; set; }

        public byte Statu { get; set; }

        public string HataAciklama { get; set; }


    }
}
