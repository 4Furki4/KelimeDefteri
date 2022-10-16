namespace KelimeDefteri.ViewModels
{
    public class PagingInfo //ViewModel for pagination tagHelper, see PaginateTagHelper where this class is used.
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

    }
}
