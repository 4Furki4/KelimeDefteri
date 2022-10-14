namespace KelimeDefteri.Models
{
    public class Definition
    {
        public long Id { get; set; }
        
        public long WordID { get; set; }
        public Word? Word { get; set; }  
        public string definition { get; set; } = string.Empty;

        public string definitionType { get; set; } = string.Empty;
    }
}
