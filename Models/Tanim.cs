namespace KelimeDefteri.Models
{
    public class Tanim
    {
        public long Id { get; set; }
        
        public long KelimeID { get; set; }
        public Kelime Kelime { get; set; }  
        public string Aciklama { get; set; } = string.Empty;

        public string AciklamaTuru { get; set; } = string.Empty;
    }
}
