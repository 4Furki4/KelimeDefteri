using KelimeDefteri.Models;

namespace KelimeDefteri.ViewModels.Defter
{
    public class RecordDetailViewModel
    {
        public RecordDetailViewModel()
        {
            Kelimeler = new List<Word>();
        }
        public int Id { get; set; }

        public DateTime date { get; set; }

        public ICollection<Word> Kelimeler { get; set; }
    }


    public class KelimeViewModel
    {
        public KelimeViewModel()
        {
            Tanimlar = new List<Definition>();
        }
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int GunlukKayitID { get; set; }
        public Record? GunlukKayit { get; set; }
        public ICollection<Definition> Tanimlar { get; set; }
    }

    public class TanimViewModel
    {
        public long Id { get; set; }

        public long KelimeID { get; set; }
        public Word? Kelime { get; set; }
        public string Aciklama { get; set; } = string.Empty;

        public string AciklamaTuru { get; set; } = string.Empty;
    }
}
