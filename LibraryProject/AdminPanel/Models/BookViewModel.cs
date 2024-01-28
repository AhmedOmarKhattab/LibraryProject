using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class BookViewModel:BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? AuthorName { get; set; }
        public int? AuthorId {  get; set; }
        public string? GenerName { get; set; }
        [Required]
        public int GenerId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Picture is Required")]

        public string PictureUrl { get; set; }
    }
}
