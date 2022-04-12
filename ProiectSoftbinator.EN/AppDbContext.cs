using Microsoft.EntityFrameworkCore;
using ProiectSoftbinator.EN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectSoftbinator.EN
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cause>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Donation>()
                .HasOne(i => i.Cause)
                .WithMany(i => i.Donations)
                .HasForeignKey(i => i.CauseId)
                .HasPrincipalKey(i => i.Id);
        }
    }
}
