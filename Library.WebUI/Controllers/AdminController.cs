using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IMainRepository _mainRepository;
        public AdminController(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }
        // GET: Admin
        public ActionResult Index()
        {
            /*if(!IsLogin())
            {
                return RedirectToAction("/Account/Login");
            }*/    
            return View();
        }
        public bool IsLogin()
        {
            var loginCookie = Request.Cookies[AccountController.loginCookie];
            if(loginCookie!=null && string.IsNullOrEmpty(loginCookie.Value)==false)
            {
                return true;
            }
            return false;
        }
        public ViewResult ProductList(int page =1, int pageSize=10)
        {
            AdminProductListViewModel model = new AdminProductListViewModel();
            model.Products = _mainRepository.Products
                .OrderByDescending(x => x.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = _mainRepository.Products.Count()
            };
            return View(model);
        }
        public ViewResult ProductView(int id)
        {
            var product = _mainRepository.Products.FirstOrDefault(x => x.ProductId == id);
            if(product == null)
            {
                return View("NotFound");
            }    
            return View(product);
        }
        public ViewResult ProductAdd()
        {
            ViewBag.Categories = _mainRepository.Categories.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductAdd(AdminProductAddModel model)
        {
            var files = Request.Files;// lấy tất cả file được gửi lên
            if(files.Count==0)
            {
                ModelState.AddModelError("", "Vui lòng thêm hình ảnh sách");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var extention = new string[] { ".jpg", ".png", ".jpeg" };
                    var file = files[0];
                    var ext = Path.GetExtension(file.FileName).ToLower();
                    if (!extention.Any(x => x== ext))
                    {
                        ModelState.AddModelError("", "Tệp tin không hợp lệ");
                    }
                    else
                    {
                        string folder = "/Content/Upload/";
                        string path = Server.MapPath("~" + folder);
                        if(!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        var destination = path+ file.FileName;
                        file.SaveAs(destination);
                        var product = new Product()
                        {
                            ProductName = model.ProductName,
                            Amount = model.Amount,
                            Price = model.Price,
                            ProductImage = "/Content/Upload/" + file.FileName,
                            CategoryId = model.CategoryId,
                            Description = model.Description
                        };
                        _mainRepository.Add(product);
                        TempData["msg"] = "thêm sản phẩm thành công";
                        ViewBag.Categories = _mainRepository.Categories.ToList();
                        return RedirectToAction(nameof(AdminController.ProductList));
                    }
                }
            }
            return View();
        }
        public ViewResult ProductEdit(int id)
        {
            var product = _mainRepository.Products.FirstOrDefault(x => x.ProductId == id);
            ViewBag.Categories = _mainRepository.Categories.ToList();
            if (product == null)
            {
                return View("NotFound");
            }
            else
            {
                var model = new AdminProductEditModel()
                {
                    Amount = product.Amount,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Price = product.Price,
                    Avatar = product.ProductImage,
                    ProductName = product.ProductName,
                    ProductId = product.ProductId
                };
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(AdminProductEditModel model)
        {
            if(Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string[] extention = new string[] { ".jpg", ".png" };
                System.Web.HttpPostedFileBase file = Request.Files[0];
                string morong = Path.GetExtension(file.FileName);
                if(!extention.Any(x=>x == morong))
                {
                    ModelState.AddModelError("", "file tải lên không hợp lệ");
                }
                else
                {
                    string folder = "/Content/Upload/";
                    string path = Server.MapPath("~" + folder);
                    if(!Directory.Exists(path))
                    {   
                        Directory.CreateDirectory(path);
                    }
                    string result = path + file;
                    file.SaveAs(result);
                    model.Avatar = "/Content/Upload/"+file.FileName;
                }
            }
            if(ModelState.IsValid)
            {
                Product product = new Product()
                {
                    Amount = model.Amount,
                    Description = model.Description,
                    Price = model.Price,
                    ProductName = model.ProductName,
                    ProductImage = model.Avatar,
                    CategoryId = model.CategoryId
                };
                _mainRepository.Edit(product);
                TempData["msg"] = "chỉnh sửa sản phẩm thành công";
                return RedirectToAction(nameof(AdminController.ProductList));
            }    
            return View(model);
        }
        public ViewResult DeleteProduct(int id)
        {
            var product = _mainRepository.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                return View(product);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(AdminDeleteProductModel model, int id)
        {
            var product = _mainRepository.Products.FirstOrDefault(x => x.ProductId == id);
            if (product != null)
            {
                _mainRepository.Remove(product);
                TempData["msg"] = "Xóa sản phẩm thành công";
            }
            return RedirectToAction(nameof(AdminController.Index));
        }
    }
}