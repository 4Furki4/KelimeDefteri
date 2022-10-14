namespace KelimeDefteri.Models
{
    public class Word
    {
        public Word()
        {
            Definitions = new List<Definition>();
        }
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int RecordId { get; set; }
        public Record? Record { get; set; } 
        public ICollection<Definition> Definitions { get; set; }

    }
}
