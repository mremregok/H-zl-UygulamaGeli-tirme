using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class SqlContext : DbContext
    {
        public DbSet<Admin>   Adminler { get; set; }
        public DbSet<Bolum>   Bolumler { get; set; }
        public DbSet<Doktor>  Doktorlar { get; set; }
        public DbSet<Hasta>   Hastalar { get; set; }
        public DbSet<Hastane> Hastaneler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Favori>  Favoriler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=hizlidoktor.database.windows.net;Initial Catalog=hizlidoktor;User ID=hizlidoktor;Password=Esas10burda;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

    }
}
