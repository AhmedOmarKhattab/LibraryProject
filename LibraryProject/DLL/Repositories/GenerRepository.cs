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
     public class GenerRepository:GenericRepository<Gener>,IGenerRepository
    {
        public GenerRepository(LibraryContext librarycontext):base(librarycontext) 
        {
            
        }
    }
}
