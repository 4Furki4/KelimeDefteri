namespace KelimeDefteri.ViewModels.Defter
{
    public class CreateRecordViewModel
    {
        public DateTime Date { get; set; }
        //public string KelimeIsmi_1 { get; set; } = string.Empty;
        //public string KelimeIsmi_2 { get; set; } = string.Empty;
        //public string KelimeIsmi_3 { get; set; } = string.Empty;
        //public string KelimeIsmi_4 { get; set; } = string.Empty;


        //public string KelimeTanimi_1 { get; set; } = string.Empty;
        //public string KelimeTanimi_2 { get; set; } = string.Empty;

        //public string KelimeTanimi_3 { get; set; } = string.Empty;
        //public string KelimeTanimi_4 { get; set; } = string.Empty;

        public List<string> Kelime_isimleri { get; set; } = new List<string>();
        public List<string> Kelime_tanimlari { get; set; } = new List<string>();

        public List<string> Kelime_turleri { get; set; } = new List<string>();

    }
}
