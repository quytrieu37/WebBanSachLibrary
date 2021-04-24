using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.WebUI.Models
{
    public class HomeSeachViewModel
    {
        public string seachString { get; set; }
        public List<Product> links { get; set; }
        [AllowHtml]
        public List<string> Description { get; set; }
    }
}