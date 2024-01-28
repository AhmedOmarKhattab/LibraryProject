using DAL.Interfaces;
using DAL.Models;
using DLL.Data;
using DLL.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
   public class BookRepository : GenericRepository<Book> ,IBookRepository
    {
        public BookRepository(LibraryContext context):base(context) 
        {
            
        }
        public async Task<IEnumerable<Book>> GetBookByNameAsync(ISpecification<Book> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
    }
}
