using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Domain.Ultilities;
using Library.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Library.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMainRepository _mainRepository;
        public AccountController(IMainRepository mainRepository)
        {
            _mainRepository = mainRepository;
        }
        // GET: Account
        public const string loginCookie = "Login";
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountLoginModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _mainRepository.Users.FirstOrDefault(x => x.UserName == model.UserName);
                if(user != null)
                {
                    var verify = Md5Helper.VerifyMd5(model.Password, user.Password);
                    if(!verify)
                    {
                        ModelState.AddModelError("", "Sai mật khẩu");
                        return View(model);
                    }
                    /*var cookie = new HttpCookie(loginCookie);
                    cookie.Value = model.UserName;
                    cookie.Expires = DateTime.Today.AddHours(1);
                    //Lưu cookie vào browser    
                    Response.Cookies.Add(cookie);*/
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return Redirect("/Admin/Index");
                }                
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại");
            }
            return View(model);
        }
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(AccountRegisterModel model)
        {
            var userName = _mainRepository.Users.FirstOrDefault(x => x.UserName == model.UserName);
            if(userName!=null)
            {
                ModelState.AddModelError("", "tên đăng nhập đã tồn tại");
                return View(model);
            }
            if(ModelState.IsValid)
            {
                var hashPassword = Md5Helper.GetMd5Hash(model.Password);
                var user = new User()
                {
                    UserName = model.UserName,
                    Password = hashPassword,
                    Email = model.Email,
                    Role = "User"
                };
                _mainRepository.Add(user);
                TempData["msg"] = "tạo tài khoản thành công";
                return View("Login");
            }    
            return View(model);
        }
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(nameof(AccountController.Login));
        }
    }
}