namespace LibraryApi.Dto
{
    public class AuthorWithBooksDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Books { get; set; }
    }
}
