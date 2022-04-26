using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Paging
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> pageItems, int count, int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Floor(count / (double)pageSize);

            AddRange(pageItems);
        }
        public static PaginatedList<T> Create(IQueryable<T> source, int? pageIndex, int pageSize = 100)
        {
            int pageIndexVal = pageIndex ?? 1;

            var count = source.Count();

            var result = source.Skip((pageIndexVal - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(result, count, pageIndexVal, pageSize);
        }
    }
}
