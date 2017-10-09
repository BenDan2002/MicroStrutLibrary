using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public class DataPage<T>
    {
        /// <summary>
        /// 页总数
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((this.TotalItems / (double)this.PageSize));
            }
        }

        /// <summary>
        /// 行总数
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// 页号码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页数据
        /// </summary>
        public List<T> PageItems { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items"></param>
        /// <param name="totalItems"></param>
        public DataPage(List<T> items, int totalItems)
        {
            this.PageItems = items;
            this.TotalItems = totalItems;

            this.PageIndex = 1;
            this.PageSize = 0;
        }
    }

    /// <summary>
    /// 分页数据扩展
    /// </summary>
    public static class DataPageExtensions
    {
        public static DataPage<T> ToPage<T>(this IQueryable<T> query, int pageNo, int pageSize) where T : class
        {
            return new DataPage<T>(query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(), query.Count())
            {
                PageIndex = pageNo,
                PageSize = pageSize
            };
        }

        public static async Task<DataPage<T>> ToPageAsync<T>(this IQueryable<T> query, int pageNo, int pageSize) where T : class
        {
            var list = await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();

            var count = await query.CountAsync();

            return new DataPage<T>(list, count)
            {
                PageIndex = pageNo,
                PageSize = pageSize
            };
        }
    }
}
