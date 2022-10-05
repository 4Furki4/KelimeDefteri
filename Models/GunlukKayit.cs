namespace KelimeDefteri.Models
{
    public class GunlukKayit
    {
        public GunlukKayit()
        {
            Kelimeler = new List<Kelime>();
        }
        public int Id { get; set; }

        public DateTime date { get; set; }

        public ICollection<Kelime> Kelimeler { get; set; }
    }
}
