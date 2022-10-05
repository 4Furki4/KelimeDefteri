using Microsoft.EntityFrameworkCore;

namespace KelimeDefteri.Models
{
    public class DefterDB : DbContext
    {
        public DefterDB(DbContextOptionsBuilder<DefterDB> optionsBuilder) : base() { }        
    }
}
