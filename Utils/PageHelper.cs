﻿namespace SocialEmpires.Utils
{
    public static class PageHelper
    {
        public static (int pageCount, IEnumerable<T>? data) Page<T>()
        {
            return (0, null);
        }

        public static (int pageCount, IEnumerable<T>? data) Page<T>(
            int pageIndex,
            int pageSize,
            IEnumerable<T> data)
        {
            var pageCount = (int)Math.Ceiling((double)data.Count() / pageSize);
            var items = data.Skip(pageIndex * pageSize).Take(pageSize);
            return (pageCount, items);
        }

        public static IQueryable<T> Page<T>(
            this IQueryable<T> query, 
            int pageIndex, 
            int pageSize, 
            out int pageCount)
        {
            pageCount = (int)Math.Ceiling((double)query.Count() / pageSize);
            return query.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}
