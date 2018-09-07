using Adventurers.Design.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreAdventure.UOW
{
    public static class PageListExtension
    {
        public static async Task<PageList<T>> ToPageListAsync<T>(this IQueryable<T> source, 
            int pageNumber, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            var totalCount = await source.CountAsync();
            var totalPage = Math.Ceiling((double)totalCount / pageSize);

            var skip = (pageNumber - 1) * pageSize;
            var items = await source.Skip(skip).Take(pageSize).ToListAsync();

            return new PageList<T>(items, pageNumber, pageSize, totalPage);
        }
    }
}
