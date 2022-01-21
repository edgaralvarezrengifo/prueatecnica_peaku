using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruebatecnicapeaku.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParametersPaginResponse<T>(this  HttpContext context,
            IQueryable<T> queryable, int nrecords)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            else
            {
                double count = await queryable.CountAsync();
                double totalPages = Math.Ceiling(count / nrecords);
                context.Response.Headers.Add("countrecords", count.ToString());
                context.Response.Headers.Add("totalpages", totalPages.ToString());
            }
        }
    }
}
