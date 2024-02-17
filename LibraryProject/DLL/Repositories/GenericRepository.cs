using DAL.Models;
using DLL.Data;
using DLL.Specifications;
using LibraryRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public class GenericRepository<T> : IGenericReopsitory<T> where T : BaseEntity
    {
        protected readonly LibraryContext _context;
        public GenericRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
          return await _context.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().Where(T=>T.Id==id).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        protected  IQueryable<T> ApplySpecification(ISpecification<T>spec)
        {
            return  SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }
    }
}
