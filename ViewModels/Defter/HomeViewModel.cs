using System.ComponentModel.DataAnnotations;

namespace KelimeDefteri.ViewModels.Defter
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "Lütfen aramak istediğiniz kaydı veya tarihi giriniz!")]
        public string searchInput { get; set; } = string.Empty;

        public long TotalWordCount { get; set; }

        public int TotalRecordCount { get; set; }

        public string deletedRecordDate { get; set; } = string.Empty;
    }
}
