﻿namespace CGEODP.Core.DomainObjects
{
    public class PagedResult<T>
    {
        public List<T> Results { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }

}