using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages
        {
            get => (int)Math.Ceiling(Convert.ToDecimal(TotalItems) / PageSize);
        }
    }
}