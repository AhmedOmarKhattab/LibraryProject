using DAL.Models;
using DLL.Specifications;
using LibraryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
   public interface IBookRepository:IGenericReopsitory<Book>
    {
        public Task<IEnumerable<Book>> GetBookByNameAsync(ISpecification<Book> spec);
    }
}
