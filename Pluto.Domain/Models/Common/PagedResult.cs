using System;
using System.Collections.Generic;
using System.Text;

namespace Pluto.Domain.Models.Common
{
    public class PagedResult<TEntity>
    {
        public PagedResult(int currentPage, int totalPages, int totalItems, List<TEntity> items)
        {
            this.CurrentPage = currentPage;
            this.TotalPages = totalPages;
            this.Items = items;
            this.TotalItems = totalItems;
        }

        public virtual int CurrentPage { get; set; }
        public virtual int TotalPages { get; set; }
        public virtual int TotalItems { get; set; }
        public virtual List<TEntity> Items { get; set; }
    }
}
