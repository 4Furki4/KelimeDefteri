using KelimeDefteri.Models;

namespace KelimeDefteri.ViewModels.Defter
{
    public class RecordDetailViewModel
    {
        public RecordDetailViewModel()
        {
            Kelimeler = new List<Kelime>();
        }
        public int Id { get; set; }

        public DateTime date { get; set; }

        public ICollection<Kelime> Kelimeler { get; set; }
    }


    public class KelimeViewModel
    {
        public KelimeViewModel()
        {
            Tanimlar = new List<Tanim>();
        }
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int GunlukKayitID { get; set; }
        public GunlukKayit? GunlukKayit { get; set; }
        public ICollection<Tanim> Tanimlar { get; set; }
    }

    public class TanimViewModel
    {
        public long Id { get; set; }

        public long KelimeID { get; set; }
        public Kelime? Kelime { get; set; }
        public string Aciklama { get; set; } = string.Empty;

        public string AciklamaTuru { get; set; } = string.Empty;
    }
}
