namespace LibraryApi.Dto
{
    public class BookWithGenerAndAuthorDto
    {
        public string Title { get; set; }   
        public string? AuthorName { get; set; }
        public string Description { get; set; }
        public string Gener {  get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
    }
}
