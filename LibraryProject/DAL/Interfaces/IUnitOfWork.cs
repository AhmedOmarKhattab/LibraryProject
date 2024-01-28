using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
   public interface IUnitOfWork
    {
        public IBookRepository BookRepository { get; set; }
        public IGenerRepository GenerRepository { get; set; }
        public IAuthorRepository AuthorRepository { get; set; }
    }
}
