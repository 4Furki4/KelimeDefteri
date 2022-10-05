using Microsoft.EntityFrameworkCore;

namespace KelimeDefteri.Models
{
    public class DefterDB : DbContext
    {
        public DefterDB(DbContextOptions<DefterDB> optionsBuilder) : base(optionsBuilder) { }

        public DbSet<GunlukKayit> GunlukKayitlar => Set<GunlukKayit>();

        public DbSet<Kelime> Kelimeler => Set<Kelime>();

        public DbSet<Tanim> Tanimlar => Set<Tanim>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GunlukKayit>()
                .HasMany(gk => gk.Kelimeler)
                .WithOne(g => g.GunlukKayit).HasForeignKey(k => k.GunlukKayitID);
            modelBuilder.Entity<Tanim>().HasOne(t => t.Kelime).WithMany(k => k.Tanimlar).HasForeignKey(t => t.KelimeID);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
