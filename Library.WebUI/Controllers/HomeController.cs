using Library.Domain.Abstract;
using Library.Domain.Concrete;
using Library.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMainRepository _mainRepository;
        public HomeController(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }
        public ViewResult Main(int page = 1, int pageSize=10, int categoryId =-1)
        {
            HomeMainViewModel model = new HomeMainViewModel();
            model.Products = _mainRepository.Products
                .OrderByDescending(x => x.ProductId)
                .Skip((page - 1) * pageSize).Where(x=>x.CategoryId == categoryId || categoryId == -1)
                .ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult SearchBook(HomeSeachViewModel model)
        {
            var links = _mainRepository.Products
                .Where(x => x.ProductName
                .Contains(model.seachString) || x.Category.CategoryName.Contains(model.seachString)).ToList();
            if(links!=null)
            {
                model.links = links;
                return View(model);
            }    
            return View("NotFound");
        }
        [HttpPost]
        public ViewResult OrderManage(string Email)
        {
            var customer = _mainRepository.Customers.FirstOrDefault(x => x.Email == Email);
            if(customer!= null)
            {
                var orders = _mainRepository.Orders.Where(x => x.CustomerId == customer.CustomerId).ToList();
                if(orders!= null)
                {
                    HomeOrderManageModel model = new HomeOrderManageModel()
                    {
                        Orders = orders,
                        customer = customer
                    };
                    ViewBag.PaymentMethod = _mainRepository.Payments.ToList();
                    return View(model);
                }
            }    
            return View("NotFound");
        }
        public ActionResult ViewOrder(int OrderId)
        {
            var ordetail = _mainRepository.OrderDetails.Where(x => x.OrderId == OrderId).ToList();
            if(ordetail!=null)
            {
                HomeViewOrderModel model = new HomeViewOrderModel();
                model.orderDetails = ordetail;
                ViewBag.Product = _mainRepository.Products.ToList();
                return View(model);
            }    
            return View();
        }
    }
}