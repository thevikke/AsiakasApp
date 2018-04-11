using AsiakasApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsiakasApp.Data
{
    public class AsiakasContext : DbContext
    {
        public AsiakasContext(DbContextOptions<AsiakasContext> options) : base(options)
        { }

        public DbSet<Asiakas> Asiakkaat { get; set; }
        public DbSet<Asiakastyyppi> AsiakasTyypit { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asiakas>().ToTable("Asiakas");

            modelBuilder.Entity<Asiakastyyppi>().ToTable("Asiakastyyppi");
        }

    }
}
