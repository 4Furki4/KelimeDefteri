using Microsoft.EntityFrameworkCore;

namespace KelimeDefteri.Models
{
    public class DefterDB : DbContext
    {
        public DefterDB(DbContextOptions<DefterDB> optionsBuilder) : base(optionsBuilder) { }

        public DbSet<Record> Records => Set<Record>();

        public DbSet<Word> Words => Set<Word>();

        public DbSet<Definition> Definitions => Set<Definition>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .HasMany(gk => gk.Words)
                .WithOne(g => g.Record).HasForeignKey(k => k.RecordId);
            modelBuilder.Entity<Definition>().HasOne(t => t.Word).WithMany(k => k.Definitions).HasForeignKey(t => t.WordID);
            modelBuilder.Entity<Record>().HasData(new Record { Id = 1, date = DateTime.Now });
            modelBuilder.Entity<Word>().HasData(
                new Word { Id = 1, RecordId = 1, Name = "Kelime" },
                new Word { Id = 2, RecordId = 1, Name = "Nutuk" },
                new Word { Id = 3, RecordId = 1, Name = "Hamaset" },
                new Word { Id = 4, RecordId = 1, Name = "Lafebesi" }
                );

            modelBuilder.Entity<Definition>().HasData(
                new Definition { Id = 1, WordID = 1, definition = "Anlamlı ses veya ses birliği, söz, sözcük, lügat", definitionType = "isim" },
                new Definition { Id = 2, WordID = 2, definition = "Söz, konuşma", definitionType = "isim" },
                new Definition { Id = 3, WordID = 2, definition = "Söylev", definitionType = "isim" },
                new Definition { Id = 4, WordID = 3, definition = "Yiğitlik, kahramanlık, cesaret", definitionType = "isim, eskimiş" },
                new Definition { Id = 5, WordID = 3, definition = "Dinleyenleri etkilemek veya heyecanlandırmak amacıyla yapılan abartılı anlatım", definitionType = "isim, eskimiş"},
                new Definition { Id = 6, WordID = 4, definition = "Çok konuşan, herkese laf yetiştiren kimse, dil ebesi, söz ebesi", definitionType = "isim, mecaz" }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
