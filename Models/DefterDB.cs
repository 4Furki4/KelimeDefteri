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
            modelBuilder.Entity<GunlukKayit>().HasData(new GunlukKayit { Id = 1, date = DateTime.Now });
            modelBuilder.Entity<Kelime>().HasData(
                new Kelime { Id = 1, GunlukKayitID = 1, Name = "Kelime" },
                new Kelime { Id = 2, GunlukKayitID = 1, Name = "Nutuk" },
                new Kelime { Id = 3, GunlukKayitID = 1, Name = "Hamaset" },
                new Kelime { Id = 4, GunlukKayitID = 1, Name = "Lafebesi" }
                );

            modelBuilder.Entity<Tanim>().HasData(
                new Tanim { Id = 1, KelimeID = 1, Aciklama = "Anlamlı ses veya ses birliği, söz, sözcük, lügat", AciklamaTuru = "isim" },
                new Tanim { Id = 2, KelimeID = 2, Aciklama = "Söz, konuşma", AciklamaTuru = "isim" },
                new Tanim { Id = 3, KelimeID = 2, Aciklama = "Söylev", AciklamaTuru = "isim" },
                new Tanim { Id = 4, KelimeID = 3, Aciklama = "Yiğitlik, kahramanlık, cesaret", AciklamaTuru = "isim, eskimiş" },
                new Tanim { Id = 5, KelimeID = 3, Aciklama = "Dinleyenleri etkilemek veya heyecanlandırmak amacıyla yapılan abartılı anlatım", AciklamaTuru = "isim, eskimiş"},
                new Tanim { Id = 6, KelimeID = 4, Aciklama = "Çok konuşan, herkese laf yetiştiren kimse, dil ebesi, söz ebesi", AciklamaTuru = "isim, mecaz" }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
