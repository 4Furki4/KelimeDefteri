using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KelimeDefteri.ViewModels.Defter
{
    public class CreateRecordViewModel
    {
        [Required(ErrorMessage = "Lütfen tarih giriniz!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Lütfen kelime giriniz!")]
        [MinLength(2, ErrorMessage ="Kelime uzunluğu en az 2 karakter olmalıdır.")]
        public string WordName1 { get; set; } = String.Empty;

        [Required(ErrorMessage = "Lütfen kelime giriniz!")]
        [MinLength(2, ErrorMessage = "Kelime uzunluğu en az 2 karakter olmalıdır.")]
        public string WordName2 { get; set; } = String.Empty;

        [Required(ErrorMessage = "Lütfen kelime giriniz!")]
        [MinLength(2, ErrorMessage = "Kelime uzunluğu en az 2 karakter olmalıdır.")]
        public string WordName3 { get; set; } = String.Empty;

        [Required(ErrorMessage = "Lütfen kelime giriniz!")]
        [MinLength(2, ErrorMessage = "Kelime uzunluğu en az 2 karakter olmalıdır.")]

        public string WordName4 { get; set; } = String.Empty;



        [Required(ErrorMessage = "Lütfen tanım giriniz")]
        [MinLength(3, ErrorMessage = "Tanım en az 3 karakterden oluşmalı.")]
        [MaxLength(300)]
        public string Definition1 { get; set; } = String.Empty;



        [Required(ErrorMessage = "Lütfen tanım giriniz")]
        [MinLength(3, ErrorMessage = "Tanım en az 3 karakterden oluşmalı.")]
        [MaxLength(300)]
        public string Definition2 { get; set; } = String.Empty;



        [Required(ErrorMessage = "Lütfen tanım giriniz")]
        [MinLength(3, ErrorMessage = "Tanım en az 3 karakterden oluşmalı.")]
        [MaxLength(300)]
        public string Definition3 { get; set; } = String.Empty;



        [Required(ErrorMessage = "Lütfen tanım giriniz")]
        [MinLength(3, ErrorMessage = "Tanım en az 3 karakterden oluşmalı.")]
        [MaxLength(300)]
        public string Definition4 { get; set; } = String.Empty;



        [Required(ErrorMessage = "Lütfen tür giriniz")]
        [MinLength(2, ErrorMessage = "Tür en az 2 karakterden oluşmalı.")]
        [MaxLength(100, ErrorMessage = "Tür en fazla 100 karakterden oluşmalı")]
        public string Type1 { get; set;} = String.Empty;


        [Required(ErrorMessage = "Lütfen tür giriniz")]
        [MinLength(2, ErrorMessage = "Tür en az 2 karakterden oluşmalı.")]
        [MaxLength(100, ErrorMessage = "Tür en fazla 100 karakterden oluşmalı")]
        public string Type2 { get; set;} = String.Empty;


        [Required(ErrorMessage = "Lütfen tür giriniz")]
        [MinLength(2, ErrorMessage = "Tür en az 2 karakterden oluşmalı.")]
        [MaxLength(100, ErrorMessage = "Tür en fazla 100 karakterden oluşmalı")]
        public string Type3 { get; set;} = String.Empty;

        [Required(ErrorMessage = "Lütfen tür giriniz")]
        [MinLength(2, ErrorMessage = "Tür en az 2 karakterden oluşmalı.")]
        [MaxLength(100, ErrorMessage = "Tür en fazla 100 karakterden oluşmalı")]
        public string Type4 { get; set;} = String.Empty;

    }


    public class NewRecordViewModel
    {
        [Required]
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
