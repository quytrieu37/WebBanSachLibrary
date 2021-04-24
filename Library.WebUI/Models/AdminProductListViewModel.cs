using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebUI.Models
{
    public class AdminProductListViewModel
    {
        public List<Product> Products { get; set; }
        public PagingInfo pagingInfo { get; set; }
    }
}