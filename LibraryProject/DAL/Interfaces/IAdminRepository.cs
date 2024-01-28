using DAL.Models;
using LibraryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAdminRepository<T>:IDisposable,IUnitOfWork where T : BaseEntity 
    {
        public Task Add(T item);
        public void Update(T Item);
        public void Delete(T item);
        public Task<int> Complete();
        
    }
}
