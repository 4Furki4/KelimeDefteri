using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KelimeDefteri.ViewModels.Defter
{
    public class CreateRecordViewModel
    {
        public DateTime Date { get; set; }
        [MinLength(2, ErrorMessage ="Kelime uzunluğu en az 2 karakter olmalıdır.")]
        public string WordName1 { get; set; }
        public string Definition1 { get; set; }
        public string Type1 { get; set; }
        public string WordName2 { get; set; }
        public string Definition2 { get; set; }
        public string Type2 { get; set; }
        public string WordName3 { get; set; }
        public string Definition3 { get; set; }
        public string Type3 { get; set; }
        public string WordName4 { get; set; }
        public string Definition4 { get; set; }
        public string Type4 { get; set; }
    }


    public class NewRecordViewModel
    {
        public List<string> newDefinition1 { get; set; } = new List<string>();
        public List<string> newDefinition2 { get; set; } = new List<string>();
        public List<string> newDefinition3 { get; set; } = new List<string>();
        public List<string> newDefinition4 { get; set; } = new List<string>();

        public List<string> newType1 { get; set; } = new List<string>();
        public List<string> newType2 { get; set; } = new List<string>();
        public List<string> newType3 { get; set; } = new List<string>();
        public List<string> newType4 { get; set; } = new List<string>();
    }
}
