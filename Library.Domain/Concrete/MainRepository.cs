using Library.Domain.Abstract;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Concrete
{
    public class MainRepository : IMainRepository
    {
        public LibraryEntities _context;
        public MainRepository()
        {
            _context = new LibraryEntities();
        }

        public IQueryable<Product> Products => _context.Products;

        public IQueryable<Category> Categories => _context.Categories;

        public IQueryable<Payment> Payments => _context.Payments;

        public IQueryable<Customer> Customers => _context.Customers;

        public IQueryable<User> Users => _context.Users;

        public IQueryable<Order> Orders => _context.Orders;

        public IQueryable<OrderDetail> OrderDetails => _context.orderDetails;

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Add(OrderDetail orderDetail)
        {
            _context.orderDetails.Add(orderDetail);
            _context.SaveChanges();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        public void Edit(Product product)
        {
            _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
