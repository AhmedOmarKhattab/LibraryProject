using DAL.Models;
using DLL.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query,ISpecification<T>spec)
        {
            if(spec.Criteria is not null)
                query=query.Where(spec.Criteria);
            if(spec.Includes is  not null)
                query=spec.Includes.Aggregate(query,(cur,next)=>cur.Include(next));
            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            if (spec.OrderByDesc is not null)
                query=query.OrderByDescending(spec.OrderByDesc);
                return query;

        }

    }
}
