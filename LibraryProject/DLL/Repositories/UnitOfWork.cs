using DAL.Interfaces;
using DLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBookRepository BookRepository { get; set; }
        public IGenerRepository GenerRepository { get; set; }
        public IAuthorRepository AuthorRepository { get; set; }
        public UnitOfWork(LibraryContext libraryContext)
        {
            BookRepository=new BookRepository(libraryContext);
            GenerRepository=new GenerRepository(libraryContext);
            AuthorRepository=new AuthorRepository(libraryContext);
        }
    }
            
}
