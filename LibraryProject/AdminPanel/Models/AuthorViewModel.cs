using DAL.Models;

namespace AdminPanel.Models
{
    public class AuthorViewModel:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
