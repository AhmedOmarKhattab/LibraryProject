using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Specifications
{
    public class AuthorSpecifications:BaseSpecification<Author>
    {
        public AuthorSpecifications()
        {
            Includes.Add(A => A.Books);
        }
        public AuthorSpecifications(Expression<Func<Author,bool>> criteria)
        {
            Includes.Add(A => A.Books);
            Criteria = criteria;
        }

    }
}
