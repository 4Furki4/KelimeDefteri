namespace KelimeDefteri.ViewModels.Defter
{
    public class CreateRecordViewModel
    {
        public DateTime Date { get; set; }
        
        public string WordName { get; set; }
        public CreateWordDefinitionViewModel Definition { get; set; }
    }


    public class CreateWordDefinitionViewModel
    {
        public string WordDefinition { get; set; }

        public string WordDefinitionType { get; set; }
    }

    //public class CreateRecordWordViewModel
    //{
    //    public string Name { get; set; }

    //    public CreateRecordDefinitionViewModel Definitions { get; set; }

    //}

    //public class CreateRecordDefinitionViewModel
    //{
    //    public string Definition { get; set; }

    //    public string Type { get; set; }
    //}
}
