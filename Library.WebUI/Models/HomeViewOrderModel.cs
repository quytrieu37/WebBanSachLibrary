using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.WebUI.Models
{
    public class HomeViewOrderModel
    {
        public List<OrderDetail> orderDetails { get; set; }
    }
}