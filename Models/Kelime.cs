namespace KelimeDefteri.Models
{
    public class Kelime
    {
        public Kelime()
        {
            Tanimlar = new List<Tanim>();
        }
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int GunlukKayitID { get; set; }
        public GunlukKayit GunlukKayit { get; set; } 
        public ICollection<Tanim> Tanimlar { get; set; }

    }
}
