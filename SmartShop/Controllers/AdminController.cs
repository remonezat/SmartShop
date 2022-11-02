using Newtonsoft.Json;
using SmartShop.Models;
using SmartShop.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class AdminController : BaseController
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: Admin
        public ActionResult Login()
        {
            ViewBag.LoginError = "Empty";
            
            if (TempData["LoginError"] != null)
            {
                ViewBag.LoginError = TempData["LoginError"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {

            var user1 = db.Users.Where(x => x.UserName == user.UserName && x.UserPassword == user.UserPassword).FirstOrDefault();

            if (user1 == null)
            {
                TempData["LoginError"] = "اسم المستخدم او كملة المرور غير صحيحة ";

                return RedirectToAction("Login", "Admin");
            }
            if (user1 != null)
            {
                ViewBag.Rest = "Solution Egypt";
                User UserIn = new User()
                {
                    Id = user1.Id,
                    UserName = user1.UserName,
                    UserPassword = user1.UserPassword,

                };
                UserLogin(UserIn);

            }


            return RedirectToAction("Home");
        }
        public ActionResult Home()
        {

            var cookie = HttpContext.Request.Cookies.Get("AdminInfo");
            if (cookie != null)
            {
                var UserIn = JsonConvert.DeserializeObject<User>(Authentication.Decrypt(cookie.Value));
                if (UserIn != null)
                {


                    return View();

                }
                else
                {
                    return RedirectToAction("Login", "Admin");

                }
            }
            else
            {
                return RedirectToAction("Login", "Admin");

            }

        }
        public ActionResult Logout()
        {
            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("AdminInfo", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            return RedirectToAction("Login");
        }

        void UserLogin(User user)
        {
            var Serialize = JsonConvert.SerializeObject(user);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("AdminInfo", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Response.Cookies.Add(authCookie);
        }
    }
}