using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page, int perPage = 10)
        {
            return source.Skip((page - 1) * perPage)
                         .Take(perPage);
        }
    }
}
