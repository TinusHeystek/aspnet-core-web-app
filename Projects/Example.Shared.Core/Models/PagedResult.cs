using System.Collections.Generic;

namespace Example.Shared.Core.Models
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; } 
        public int PageCount { get; set; } 
        public int PageSize { get; set; } 
        public int RowCount { get; set; }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }
 
        public PagedResult()
        {
            Results = new List<T>();
        }

        public PagedResult(int currentPage, int pageSize, int rowCount) : this()
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            RowCount = rowCount;
        }
    }
}