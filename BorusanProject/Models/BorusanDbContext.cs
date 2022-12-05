using BorusanServices.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BorusanServices.Models
{
    public class BorusanDbContext : DbContext
    {
        public BorusanDbContext(DbContextOptions<BorusanDbContext> options)
    : base(options)
        {

        }
        public  DbSet<Malzeme> Malzeme { get; set; }
        public  DbSet<Siparis> Siparis { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Siparis>()
                .HasIndex(s=> s.MusteriSiparisNo)
                .IsUnique();
        }
    }
}
