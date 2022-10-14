namespace KelimeDefteri.ViewModels.Defter
{
    public class CreateRecordViewModel
    {
        public DateTime Date { get; set; }
        public List<string> WordNames { get; set; } = new List<string>();
        public List<string> WordDefs { get; set; } = new List<string>();

        public List<string> WordTypes { get; set; } = new List<string>();

    }
}
