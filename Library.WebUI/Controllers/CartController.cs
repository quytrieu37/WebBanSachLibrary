using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Domain.Ultilities;
using Library.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Library.WebUI.Controllers
{
    public class CartController : Controller
    {
/*        AdminController admin = new AdminController();*/
        private readonly IMainRepository _mainRepository;
        public CartController(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }
        // GET: Cart
        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }
        [HttpPost]
        public ActionResult AddToCart(AddToCartModel model)
        {   
            if(ModelState.IsValid)
            {
                var product = _mainRepository.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
                if(product!= null)
                {
                    var cart = GetCart();
                    cart.Add(product, model.quantity);
                    return Json(new { state = true, msg = "thêm sản phẩm thành công" });
                }    
            }
            return Json(new { state = false, msg = "Thêm sản phẩm thất bại" });
        }
        [HttpPost]
        public ActionResult UpdateToCart(AddToCartModel model)
        {
            if(ModelState.IsValid)
            {
                var product = _mainRepository.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
                if(product!=null)
                {
                    var cart = GetCart();
                    cart.Update(product, model.quantity);
                }    
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var product = _mainRepository.Products.FirstOrDefault(x => x.ProductId == id);
            if(product!= null)
            {
                var cart = GetCart();
                cart.Remove(product);
            }
            return RedirectToAction("Index");
        }
        public PartialViewResult CartSummary()
        {
            var cart = GetCart();
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            ViewBag.PaymentMethods = _mainRepository.Payments.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(CartCheckoutViewModel model)
        {
            var cart = GetCart();
            if(cart.lines.Count()==0)
            {
                ModelState.AddModelError("", "Giỏ hàng rỗng");
            }    
            if(ModelState.IsValid)
            {
                //lưu vào db
                //khách hàng
                var tempcustomer = _mainRepository.Customers.FirstOrDefault(x => x.CustomerName.ToLower() == model.FullName.ToLower());
                Customer customer;
                if(tempcustomer == null)
                {
                    customer = new Customer()
                    {
                        CustomerName = model.FullName,
                        Address = model.Address,
                        Email = model.Email,
                        Phone = model.Phone
                    };
                    _mainRepository.Add(customer);
                }    
                else
                {
                    customer = tempcustomer;
                }    
                var order = new Order()
                {
                    Ship = model.UserShipper,
                    CreateDate = DateTime.Now,
                    Note = model.Note,
                    CustomerId = customer.CustomerId,
                    PaymentId = model.PaymentMethod
                };
                _mainRepository.Add(order);
                //chi tiết đơn hàng
                foreach(var item in cart.lines)
                {
                    var orderDetail = new OrderDetail()
                    {
                        Amount = item.Quantity,
                        Price = item.Product.Price,
                        OrderId = order.OrderId,
                        ProductId = item.Product.ProductId
                    };
                    _mainRepository.Add(orderDetail);
                }
                //gửi email: nội dung, chủ đề , người nhận
                //xây dựng nội dung
                StringBuilder sb = new StringBuilder();
                sb.Append("<table>");
                //full name
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("Khách hàng");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(model.FullName);
                sb.Append("</td>");
                sb.Append("</tr>");

                //email
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("Email");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(model.Email);
                sb.Append("</td>");
                sb.Append("</tr>");
                //điện thoại
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("ĐIện thoại");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(model.Phone);
                sb.Append("</td>");
                sb.Append("</tr>");
                ///địa chỉ
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("Khách hàng");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(model.FullName);
                sb.Append("</td>");
                sb.Append("</tr>");
                //thanh toán
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("Khách hàng");
                sb.Append("</td>");
                var payment = _mainRepository.Payments.FirstOrDefault(x => x.PaymentId == model.PaymentMethod);
                sb.Append("<td>");
                sb.Append(payment.PaymentName);// ở đây phải như vầy vì truyền lên client chỉ có Id nên phải xài như vầy
                sb.Append("</td>");
                sb.Append("</tr>");
                //ghi chú
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("ghi chú");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(model.Note);
                sb.Append("</td>");
                sb.Append("</tr>");

                //ship
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("Ship tận nhà");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(model.UserShipper ? ": Có." : ": Không.");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                //verify

                var sender = new EmailSender();
                sender.Send("Thông báo đặt hàng", sb.ToString(), model.Email);
                TempData["msg"] = "Đặt hàng thành công";
                cart.Clear();
                //success
                return View("ok"); // về trang chủ nếu thành công

            }
            ViewBag.PaymentMethods = _mainRepository.Payments.ToList();
            return View(model);
        }
        #region helper
        private Cart GetCart()
        {
            var cart = Session["cart"] as Cart;
            if(cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;
        }
        #endregion
    }
}