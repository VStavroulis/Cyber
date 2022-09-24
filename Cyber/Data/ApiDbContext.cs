using Cyber.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyber.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Alert> Alerts { get; set; }

        public DbSet<IPAddress> IPAddress { get; set; }

        public DbSet<AlertIP> AlertIps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alert>()
                .HasKey(a => new { a.ID });

            modelBuilder.Entity<Alert>()
                .HasIndex(a => new { a.Title, a.Description , a.Severity }).IsUnique();

            modelBuilder.Entity<IPAddress>()
                .HasKey(i => new { i.ID });

            modelBuilder.Entity<IPAddress>()
                .HasIndex(i => new { i.IP }).IsUnique();

            modelBuilder.Entity<AlertIP>()
                .HasKey(ai => new { ai.ID });

            modelBuilder.Entity<AlertIP>()
                .HasIndex(ai => new { ai.AlertId, ai.IPAddressId }).IsUnique();

            modelBuilder.Entity<AlertIP>()
                .HasOne<Alert>(a => a.Alert)
                .WithMany(a => a.AlertIPs)
                .HasForeignKey(a => a.AlertId);


            modelBuilder.Entity<AlertIP>()
                .HasOne<IPAddress>(i => i.IPAddress)
                .WithMany(i => i.AlertIPs)
                .HasForeignKey(i => i.IPAddressId);
        }
    }
}
