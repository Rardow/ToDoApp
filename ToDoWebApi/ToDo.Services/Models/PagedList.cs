using System.Collections.Generic;

namespace ToDo.Services.Models
{
    /// <summary>
    /// Paged result of T type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T>
    {
        /// <summary>
        /// Items of T type
        /// </summary>
        public IList<T> Items { get; set; }

        /// <summary>
        /// Count of pages
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Actual page index
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Page size 
        /// </summary>
        public int PageSize { get; set; }
    }
}
