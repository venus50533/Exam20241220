using Microsoft.EntityFrameworkCore;
using SqlServerJsonWebAPI.Models;

namespace YourNamespace.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MyOfficeACPD> MyOfficeACPDs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyOfficeACPD>(entity =>
            {
                entity.HasKey(e => e.ACPD_SID);
                entity.Property(e => e.ACPD_Cname).HasMaxLength(60);
                entity.Property(e => e.ACPD_Ename).HasMaxLength(40);
                entity.Property(e => e.ACPD_Status);
                entity.Property(e => e.ACPD_LoginID).HasMaxLength(30);
                entity.Property(e => e.ACPD_LoginPWD).HasMaxLength(60);
            });
        }
    }
}
