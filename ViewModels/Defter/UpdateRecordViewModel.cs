using KelimeDefteri.Models;

namespace KelimeDefteri.ViewModels.Defter
{
    public class UpdateRecordViewModel
    {
        public UpdateRecordViewModel()
        {
            Words = new List<Word>();
        }
        public int Id { get; set; }

        public DateTime date { get; set; }

        public ICollection<Word> Words { get; set; }
    }


    public class WordUpdateViewModel
    {
        public WordUpdateViewModel()
        {
            Definitions = new List<Definition>();
        }
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int RecordID { get; set; }
        public Record? Record { get; set; }
        public List<Definition> Definitions { get; set; } = new List<Definition>();
    }

    public class DefinitionUpdateViewModel
    {
        public long Id { get; set; }

        public long WordID { get; set; }
        public Word? Word { get; set; }
        public string definition { get; set; } = string.Empty;

        public string definitionType { get; set; } = string.Empty;
    }
}
