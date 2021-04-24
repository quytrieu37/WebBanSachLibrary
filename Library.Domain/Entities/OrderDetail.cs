﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class OrderDetail
    {
        [Key]
        [Column(Order=1)]
        public int OrderId { get; set; }
        [Key]
        [Column(Order =2)]
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
