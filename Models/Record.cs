namespace KelimeDefteri.Models
{
    public class Record
    {
        public Record()
        {
            Words = new List<Word>();
        }
        public int Id { get; set; }

        public DateTime date { get; set; }

        public ICollection<Word> Words { get; set; }
    }
}
