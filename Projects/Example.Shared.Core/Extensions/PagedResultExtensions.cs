using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Example.Shared.Core.Models;

namespace Example.Shared.Core.Extensions
{
    public static class PagedResultExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            int i;
            try
            {
                i = query.Count();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var result = new PagedResult<T>(currentPage, pageSize, i );

            var calc = result.CalculatePageCountAndSkip();
            result.PageCount = calc.pageCount;
            result.Results = query
                .Skip(calc.skip)
                .Take(pageSize)
                .ToList();

            return result;
        }

        public static PagedResult<TModel> GetPaged<TDbSet, TModel>(this IQueryable<TDbSet> query, IMapper mapper,
            int currentPage, int pageSize) where TModel: class
        {
            var result = new PagedResult<TModel>(currentPage, pageSize, query.Count());
            var calc = result.CalculatePageCountAndSkip();
            result.PageCount = calc.pageCount;
            result.Results = query
                .Skip(calc.skip)
                .Take(pageSize)
                .ProjectTo<TModel>(mapper.ConfigurationProvider)
                .ToList();
 
            return result;
        }

        private static (int pageCount, int skip) CalculatePageCountAndSkip(this PagedResultBase pagedResult) 
        {
            var pageCountDouble = (double)pagedResult.RowCount / pagedResult.PageSize;
            var pageCount = (int)Math.Ceiling(pageCountDouble);
            var skip = (pagedResult.CurrentPage - 1) * pagedResult.PageSize;
            return (pageCount, skip);
        }
    }
}