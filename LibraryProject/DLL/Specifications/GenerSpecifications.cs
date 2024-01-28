using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Specifications
{
    public class GenerSpecifications:BaseSpecification<Gener>
    {
        public GenerSpecifications()
        {
            Includes.Add(G => G.Books);
        }
        public GenerSpecifications(Expression<Func<Gener,bool>> criteria)
        {
            Includes.Add(G => G.Books);
            Criteria = criteria;
        }
    }
}
