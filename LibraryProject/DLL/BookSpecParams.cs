using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL
{
    public class BookSpecParams
    {
        private const int MaxSize= 10;
        private int pagesize;
        public int PageIndex { set; get; } = 1;
       public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value>MaxSize?MaxSize:value; }
        }

        public string? OrderBy { get; set; }
        public int? GenerId { get; set; }
        public int? AuthorId { get; set;}
    }
}
