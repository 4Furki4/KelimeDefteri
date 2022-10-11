namespace KelimeDefteri.ViewModels.Defter
{
    public class RecordDetailViewModel
    {
        public DateTime Date { get; set; }
        public List<string> KelimeAdlari { get; set; } = new List<string>();

        public List<string> Tanimlari { get; set; } = new List<string>();

        public List<string> TanimTurleri { get; set; } = new List<string>();
    }
}
