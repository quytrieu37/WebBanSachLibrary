using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string Note { get; set; }
        public int PaymentId { get; set; }
        public bool Ship { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
