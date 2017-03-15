using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Classes
{
    public class PagedList<T>
    {
        public int PageIndex { get; private set; }


        public int PageSize { get; private set; }


        public int TotalCount { get; private set; }


        public int TotalPages { get; private set; }


        public List<T> PagedItems { get; private set; }




        /// <summary>
        /// This constructor to be used when using database paging
        /// </summary>
        /// <param name="data"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalCount"></param>
        public PagedList(List<T> data, int page, int pagesize, int totalCount)
        {
            PageIndex = page;
            PageSize = pagesize;
            PagedItems = data;


            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
        }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }


        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

    }
}