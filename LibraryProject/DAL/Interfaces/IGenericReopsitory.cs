using DAL.Models;
using DLL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepositories.Interfaces
{
    public interface IGenericReopsitory<T> where T : BaseEntity
    {
        public Task<T> GetAsync(int id);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T> GetWithSpecAsync(ISpecification<T> spec);
        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T>spec);
    }
}
