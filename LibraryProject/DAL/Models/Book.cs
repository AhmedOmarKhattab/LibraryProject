using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Book:BaseEntity
    {
        public string Title { get; set; }   
        public string Description { get; set; }
        public int? AuthorId { get; set; }
        public int GenerId { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public Author Author { get; set; }
        public Gener Gener { get; set; }
    }
}
