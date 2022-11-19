using Newtonsoft.Json;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShop.Utilites;

namespace SmartShop.Controllers
{
    public class BaseController : Controller
    {
        SmartShopEntities1 db = new SmartShopEntities1();

        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            var cookie = HttpContext.Request.Cookies.Get("AdminInfo");
            if (cookie != null)
            {
                var UserIn = JsonConvert.DeserializeObject<User>(Authentication.Decrypt(cookie.Value));
                if (UserIn != null)
                {
                    var SelectUserScreens = db.Users.Where(x => x.Id == UserIn.Id).Select(x => x.UserScreens).FirstOrDefault();

                    string[] strArray = SelectUserScreens.Split(',');
                    ViewBag.userscreens = strArray;
                    ViewBag.username  = db.Users.Where(x => x.Id == UserIn.Id).Select(x => x.UserName).FirstOrDefault();
                    ;

                }
            }
        }


    }
}