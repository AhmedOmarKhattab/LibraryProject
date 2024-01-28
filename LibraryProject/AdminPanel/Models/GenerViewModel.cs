using DAL.Models;

namespace AdminPanel.Models
{
    public class GenerViewModel:BaseEntity
    {
        public string Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}
