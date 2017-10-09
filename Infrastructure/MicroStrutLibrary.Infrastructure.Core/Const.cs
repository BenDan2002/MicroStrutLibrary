using System;
using System.Collections.Generic;
using System.Text;

namespace MicroStrutLibrary.Infrastructure.Core
{
    public sealed class Const
    {
        /// <summary>
        /// 系统最小日期
        /// </summary>
        public static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        /// <summary>
        /// 系统最大日期
        /// </summary>
        public static readonly DateTime MaxDate = new DateTime(2999, 12, 31);
        /// <summary>
        /// 长期缓存的最大时间
        /// </summary>
        public static readonly TimeSpan MaxCacheSpanTime = new TimeSpan(7, 0, 0, 0);
        /// <summary>
        /// 系统月日日期格式。[MM-dd]
        /// </summary>
        public const string MonthDayFormat = "MM-dd";
        /// <summary>
        /// 系统短日期格式。[yyyy-MM-dd]
        /// </summary>
        public const string ShortDateFormat = "yyyy-MM-dd";
        /// <summary>
        /// 系统长日期时间格式。[yyyy-MM-dd HH:mm:ss]
        /// </summary>
        public const string LongDateFormat = "yyyy-MM-dd HH:mm:ss";
    }
}
