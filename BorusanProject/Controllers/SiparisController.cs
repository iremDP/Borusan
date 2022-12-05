using BorusanServices.Models;
using BorusanServices.Models.Dto;
using BorusanServices.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorusanServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SiparisController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        DbContextOptionsBuilder<BorusanDbContext> _optionsBuilder;
        private BorusanDbContext _kuyrukDb;

        private readonly BorusanDbContext _db;

        public SiparisController(BorusanDbContext context, IConfiguration configuration)
        {
            _db = context;

            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<BorusanDbContext>();
            _connectionString = _configuration.GetConnectionString("MsSQLConnection");
            _optionsBuilder.UseSqlServer(_connectionString);

        }

        [HttpGet]
        public IActionResult GetirSiparisListesi()
        {
            return Ok(_db.Siparis.AsNoTracking().ToList());
        }

        [HttpGet]
        public IActionResult GetirSiparisMusteriSiparisNoIle(string musteriSiparisNo)
        {
            var result = _db.Siparis.AsNoTracking().ToList().FirstOrDefault(x => x.MusteriSiparisNo == musteriSiparisNo);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetirSiparisSistemSiparisNoIle(int sistemSiparisNo)
        {
            var result = _db.Siparis.AsNoTracking().ToList().FirstOrDefault(x => x.SistemSiparisNo == sistemSiparisNo);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult EkleSiparis(List<DtoSiparis> siparisListesi)
        {
            List<DtoSiparisCevap> cevapListesi = new List<DtoSiparisCevap>();
            foreach (var item in siparisListesi)
            {
                try
                {
                    if (!_db.Malzeme.AsNoTracking().Any(x => x.MalzemeKodu == item.MalzemeKodu))
                    {
                        Malzeme dbMalzeme = new Malzeme
                        {
                            MalzemeKodu = item.MalzemeKodu,
                            MalzemeAdi = item.MalzemeAdi
                        };

                        _db.Malzeme.Add(dbMalzeme);
                        _db.SaveChanges();
                    }
                    if (!_db.Siparis.AsNoTracking().Any(x => x.MusteriSiparisNo == item.MusteriSiparisNo))
                    {
                        Siparis dbSiparis = new Siparis
                        {
                            MusteriSiparisNo = item.MusteriSiparisNo,
                            CikisAdresi = item.CikisAdresi,
                            VarisAdresi = item.VarisAdresi,
                            Miktar = item.Miktar,
                            MiktarBirim = item.MiktarBirim,
                            Agirlik =item.Agirlik,
                            AgirlikBirim = item.AgirlikBirim,
                            MalzemeKodu = item.MalzemeKodu,
                            Not = item.Not,
                            SiparisDurum = Models.Enum.SiparisDurums.SiparisAlindi,
                            DegisimTarihi = DateTime.Now
                        };

                        _db.Siparis.Add(dbSiparis);
                        var id = _db.SaveChanges();

                        DtoSiparisCevap dtoCevap = (DtoSiparisCevap)item;

                        dtoCevap.SistemSiparisNo = id;
                        dtoCevap.SiparisDurum = dbSiparis.SiparisDurum;
                        dtoCevap.DegisimTarihi = dbSiparis.DegisimTarihi;

                    }
                    else
                    {
                        DtoSiparisCevap dtoCevap = (DtoSiparisCevap)item;
                        dtoCevap.Statu = 1;
                        dtoCevap.HataAciklama = " Aynı Sipariş Numarası Bulunmaktadır.";
                    }
                }
                catch (Exception ex)
                {
                    DtoSiparisCevap dtoCevap = (DtoSiparisCevap)item;
                    dtoCevap.Statu = 1;
                    dtoCevap.HataAciklama = ex.ToString();

                }
            }

            return CreatedAtAction("GetirSiparisListesi", siparisListesi);
        }

        [HttpPost]
        public async Task<IActionResult> EkleSiparisAsenkron(List<DtoSiparis> siparisListesi)
        {
            foreach (var item in siparisListesi)
            {
                try
                {
                    if (!_db.Malzeme.AsNoTracking().Any(x => x.MalzemeKodu == item.MalzemeKodu))
                    {
                        Malzeme dbMalzeme = new Malzeme
                        {
                            MalzemeKodu = item.MalzemeKodu,
                            MalzemeAdi = item.MalzemeAdi
                        };

                        _db.Malzeme.Add(dbMalzeme);
                        await _db.SaveChangesAsync();
                    }
                    if (!_db.Siparis.AsNoTracking().Any(x => x.MusteriSiparisNo == item.MusteriSiparisNo))
                    {
                        Siparis dbSiparis = new Siparis
                        {
                            MusteriSiparisNo = item.MusteriSiparisNo,
                            CikisAdresi = item.CikisAdresi,
                            VarisAdresi = item.VarisAdresi,
                            Miktar = item.Miktar,
                            MiktarBirim = item.MiktarBirim,
                            MalzemeKodu = item.MalzemeKodu,
                            Not = item.Not,
                            SiparisDurum = Models.Enum.SiparisDurums.SiparisAlindi,
                            DegisimTarihi = DateTime.Now
                        };

                        _db.Siparis.Add(dbSiparis);
                        var id = await _db.SaveChangesAsync();
                        DtoSiparisCevap dtoCevap = (DtoSiparisCevap)item;

                        dtoCevap.SistemSiparisNo = id;
                        dtoCevap.SiparisDurum = dbSiparis.SiparisDurum;
                        dtoCevap.DegisimTarihi = dbSiparis.DegisimTarihi;

                    }
                    else
                    {
                        DtoSiparisCevap dtoCevap = (DtoSiparisCevap)item;
                        dtoCevap.Statu = 1;
                        dtoCevap.HataAciklama = " Aynı Sipariş Numarası Bulunmaktadır.";
                    }
                }
                catch (Exception ex)
                {
                    DtoSiparisCevap dtoCevap = (DtoSiparisCevap)item;
                    dtoCevap.Statu = 1;
                    dtoCevap.HataAciklama = ex.ToString();

                }
            }

            return CreatedAtAction("GetirSiparisListesi", siparisListesi);
        }
        /// <summary>
        /// Rabbit mq olmadan çalışan güncelleme
        /// </summary>
        /// <param name="siparis"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GuncelleSiparisStatuSenkron(DtoSiparis siparis)
        {           
            try
            {
                var item = _db.Siparis.FirstOrDefault(x => x.MusteriSiparisNo == siparis.MusteriSiparisNo);

                if (item != null)
                {                   
                    item.SiparisDurum = siparis.SiparisDurum;
                    item.DegisimTarihi = DateTime.Now;

                    _db.Entry(item).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Ok(siparis);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                DtoSiparisCevap dtoCevap = (DtoSiparisCevap)siparis;
                dtoCevap.Statu = 1;
                dtoCevap.HataAciklama = ex.ToString();

                return BadRequest(dtoCevap);

            }

            

        }
        /// <summary>
        /// Rabbit mq ile çalışan güncelleme
        /// </summary>
        /// <param name="siparis"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GuncelleSiparisStatu(DtoSiparis siparis)
        {
            try
            {
                ekleKuyruk(siparis);
            }
            catch(Exception ex)
            {
                DtoSiparisCevap dtoCevap = new DtoSiparisCevap();
                dtoCevap.Statu = 1;
                dtoCevap.HataAciklama = ex.ToString();

                return BadRequest(dtoCevap);
            }

            return NoContent();

        }
        /// <summary>
        /// Rabbit MQ kuyruğa gönderen method
        /// </summary>
        /// <param name="siparis"></param>
        private void ekleKuyruk(DtoSiparis siparis)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Siparis", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = JsonConvert.SerializeObject(siparis);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "Siparis", basicProperties: null, body: body);

            }
        }
        /// <summary>
        /// Rabbit MQ tarafında kuyruktan gelen işlemleri işleyen method
        /// </summary>
        private void dinleKuyruk()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare
                (
                    queue: "Siparis",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, mq) =>
                {
                    var MesajGovdesi = mq.Body;
                    var mesaj = Encoding.UTF8.GetString(MesajGovdesi.ToArray());
                    var siparis = JsonConvert.DeserializeObject<DtoSiparis>(mesaj);
                    guncelleSiparisStatu(siparis);
                };

                channel.BasicConsume
                (
                    queue: "Siparis",
                    autoAck: false, // true ise mesaj otomatik olarak kuyruktan silinir
                    consumer: consumer
                );
            }
        }

        private void guncelleSiparisStatu(DtoSiparis siparis)
        {
            _kuyrukDb = new BorusanDbContext(_optionsBuilder.Options);

            var item = _db.Siparis.FirstOrDefault(x => x.MusteriSiparisNo == siparis.MusteriSiparisNo);

            if (item != null)
            {
                item.SiparisDurum = siparis.SiparisDurum;
                item.DegisimTarihi = siparis.DegisimTarihi;

                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

    }
}
