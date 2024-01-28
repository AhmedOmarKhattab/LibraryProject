using DAL.Interfaces;
using DAL.Models;
using DLL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class AuthorRepository:GenericRepository<Author>,IAuthorRepository
    {
        public AuthorRepository(LibraryContext librarycontext):base(librarycontext) 
        {
            
        }
    }
}
