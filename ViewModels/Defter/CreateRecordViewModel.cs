namespace KelimeDefteri.ViewModels.Defter
{
    public class CreateRecordViewModel
    {
        public DateTime Date { get; set; }
        public List<string> Kelime_isimleri { get; set; } = new List<string>();
        public List<string> Kelime_tanimlari { get; set; } = new List<string>();

        public List<string> Kelime_turleri { get; set; } = new List<string>();

    }
}
