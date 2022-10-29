using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KelimeDefteri.ViewModels.Defter
{
    public class CreateRecordViewModel
    {
        public DateTime Date { get; set; }

        [BindProperty]
        [Required]
        public string WordName1 { get; set; }
        [BindProperty]
        [Required]
        public string Definition1 { get; set; }
        [BindProperty]
        [Required]
        public string Type1 { get; set; }
        [BindProperty]
        [Required]
        public string WordName2 { get; set; }
        [BindProperty]
        [Required]
        public string Definition2 { get; set; }
        [BindProperty]
        [Required]
        public string Type2 { get; set; }
        [BindProperty]
        [Required]
        public string WordName3 { get; set; }
        [BindProperty]
        [Required]
        public string Definition3 { get; set; }
        [BindProperty]
        [Required]
        public string Type3 { get; set; }
        [BindProperty]
        [Required]
        public string WordName4 { get; set; }
        [BindProperty]
        [Required]
        public string Definition4 { get; set; }
        [BindProperty]
        [Required]
        public string Type4 { get; set; }
    }


    public class NewRecordViewModel
    {
        public DateTime Date { get; set; }
        public List<string> WordName { get; set; } = new List<string>();

        public List<string> Definition { get; set; } = new List<string>();
        public List<string> Type { get; set; } = new List<string>();
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
