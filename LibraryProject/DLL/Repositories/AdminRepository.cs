using DAL.Interfaces;
using DAL.Models;
using DLL.Data;
using DLL.Specifications;
using LibraryRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class AdminRepository<T> : UnitOfWork,IAdminRepository<T> where T : BaseEntity
    {
        private readonly LibraryContext _context;

        public AdminRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task Add(T item)
        {
           await _context.AddAsync(item);
        }

        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public  void Delete(T item)
        {
             _context.Remove(item);
        }

        public async void Dispose()
        {
           await  _context.DisposeAsync();
        }


       
        public async void Update(T Item)
        {
            _context.Update(Item);
        }

  
    }
}
