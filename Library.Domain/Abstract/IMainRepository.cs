using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Abstract
{
    public interface IMainRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Payment> Payments { get; }
        IQueryable<Customer> Customers { get; }
        IQueryable<User> Users { get; }
        IQueryable<Order> Orders { get; }
        IQueryable<OrderDetail> OrderDetails { get; }
        void Add(Order order);
        void Add(OrderDetail orderDetail);
        void Add(User user);
        void Add(Customer customer);
        void Add(Product product);
        void Edit(Product product);
        void Remove(Product product);
    }
}
