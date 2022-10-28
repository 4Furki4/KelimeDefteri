using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KelimeDefteri.ViewModels.Defter
{
    public class CreateRecordViewModel
    {
        public DateTime Date { get; set; }

        [BindProperty]
        [Required]
        public string WordName { get; set; }
        [BindProperty]
        [Required]
        public string Definition { get; set; }
        [BindProperty]
        [Required]
        public string Type { get; set; } 
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
