using BookLibraryClassLibrary.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.ModelResponseWrapper
{
    public class ExtraInfoWrapper<T>
    {
        public int TotalResults { get; }

        public DateTime RequestDate { get; }

        public int PageIndex { get; }

        public int TotalPages { get; }

        public List<T> Data { get; }

        public ExtraInfoWrapper(PaginatedList<T> data)
        {
            Data = data;
            TotalResults = Data.Count;
            RequestDate = DateTime.Now;
            PageIndex = data.PageIndex;
            TotalPages = data.TotalPages;
        }

        public ExtraInfoWrapper(List<T> data)
        {

            Data = data;
            TotalResults = Data.Count;
            RequestDate = DateTime.Now;
            PageIndex = 1;
            TotalPages = 1;
        }
    }
}
