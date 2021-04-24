using Library.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Cart
    {
        public List<CartLine> lines { get; private set; }
        public Cart()
        {
            lines = new List<CartLine>();
        }
        public void Add(Product product, int quantity)
        {
            var line = lines.FirstOrDefault(x => x.Product.ProductId == product.ProductId);
            if(line==null)
            {
                line = new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                };
                lines.Add(line);
            }
            line.Quantity += quantity;
        }
        public void Update(Product product, int quantity)
        {
            var line = lines.FirstOrDefault(x => x.Product.ProductId == product.ProductId);
            if(line!=null)
            {
                line.Quantity = quantity;
            }    
        }
        public void Remove(Product product)
        {
            var line = lines.FirstOrDefault(x => x.Product.ProductId == product.ProductId);
            if(line!= null)
            {
                lines.Remove(line);
            }    
        }
        public decimal Caculator()
        {
            return lines.Sum(x => x.Product.Price * x.Quantity);
        }
        public void Clear()
        {
            lines.Clear();
        }
    }
}
