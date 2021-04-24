using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebUI.Models
{
    public class HomeOrderManageModel
    {
        public List<Order> Orders { get; set; }
        public Customer customer { get; set; }
    }
}