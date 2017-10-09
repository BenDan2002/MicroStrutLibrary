using System;
using System.Collections.Generic;
using System.Text;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public abstract class QueryCondition
    {
        public string SearchKey { get; set; }
    }
    public abstract class DateQueryCondition: QueryCondition
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public abstract class PageQueryCondition : QueryCondition
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public abstract class PageDateQueryCondition: DateQueryCondition
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
