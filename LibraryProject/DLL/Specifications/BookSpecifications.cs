using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DLL.Specifications
{
    public class BookSpecifications:BaseSpecification<Book>
    {
        public BookSpecifications(BookSpecParams productspecparams):
            base(B=>(!productspecparams.GenerId.HasValue||B.GenerId==productspecparams.GenerId.Value)
            &&(!productspecparams.AuthorId.HasValue||productspecparams.AuthorId.Value==B.AuthorId))
        {
            Includes.Add(B => B.Author);
            Includes.Add(B => B.Gener);
            AddOrderBy(p => p.Title);
            if (!string.IsNullOrEmpty(productspecparams.OrderBy))
            {
                switch (productspecparams.OrderBy)
                {
                    case "priceasc":
                        AddOrderBy(p => p.Price); break;
                    case "pricedesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    case "nameasc":
                        AddOrderBy(p => p.Title);
                        break;
                    case "namedesc":
                        AddOrderByDesc(p => p.Title);
                        break;
                    default:
                        AddOrderBy(p => p.Title);
                        break;
                }

            }
        }
        public BookSpecifications(Expression<Func<Book,bool>> criteria)
        {
            Includes.Add(B => B.Author);
            Includes.Add(B => B.Gener);
            Criteria = criteria;
           


            }

    }
}
