using pruebatecnicapeaku.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pruebatecnicapeaku.Server.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Pagionation<T>(this IQueryable<T> queryable,Pagin pagin)
        {
            return queryable.Skip((pagin.page - 1) * pagin.NRecords).Take(pagin.NRecords);
        }
    }
}
