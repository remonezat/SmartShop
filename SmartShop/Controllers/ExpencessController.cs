using Newtonsoft.Json;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class ExpencessController : BaseController
    {
        SmartShopEntities1 db = new SmartShopEntities1();

        // GET: Expencess
        public ActionResult Add()
        {
            ViewBag.SuccessMessage = "Empty";
            ViewBag.DeleteMessage = "Empty";
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            if (TempData["DeleteMessage"] != null)
            {
                ViewBag.DeleteMessage = TempData["DeleteMessage"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Add(Expencess expencess)
        {
            db.Expencesses.Add(expencess);
            db.SaveChanges();
            TempData["SuccessMessage"] = " تم الحفظ !! ";

            return RedirectToAction("Add");
        }

        public ActionResult ShowExpencess()
        {
            ViewBag.SuccessMessage = "Empty";
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
           
            return View();
        }
        public JsonResult GetExpencess(DateTime DateF, DateTime DateT, string Term)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var SelectExpencess = db.Expencesses.Where(x =>x.Date >= DateF && x.Date <= DateT).ToList();

            if (!string.IsNullOrEmpty(Term))
            {
                 SelectExpencess = db.Expencesses.Where(x => x.Note.Contains(Term)&&x.Date>=DateF&&x.Date<=DateT).ToList();
            }

            return Json(SelectExpencess, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteExpence(DateTime DateF, DateTime DateT, string Term, int ExpenceId = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;


            if (ExpenceId > 0)
            {
                var selectExpence = db.Expencesses.Where(x => x.Id == ExpenceId).FirstOrDefault();
                db.Expencesses.Remove(selectExpence);
                db.SaveChanges();
            }
            var SelectExpencess = db.Expencesses.Where(x => x.Date >= DateF && x.Date <= DateT).ToList();

            if (!string.IsNullOrEmpty(Term))
            {
                SelectExpencess = db.Expencesses.Where(x => x.Note.Contains(Term) && x.Date >= DateF && x.Date <= DateT).ToList();
            }

            return Json(SelectExpencess, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Edit(int id = 0)
        {
            var Select_Expencess = db.Expencesses.Where(x => x.Id == id).FirstOrDefault();
         
            var date = Select_Expencess.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ViewData["Dt"] = date;


            return View(Select_Expencess);
        }
        [HttpPost]
        public ActionResult Edit(Expencess expencess)
        {
            //var cookie = HttpContext.Request.Cookies.Get("AdminInfo");
            //if (cookie != null)
            //{
            //    var UserIn = JsonConvert.DeserializeObject<user>(Authentication.Decrypt(cookie.Value));
            //    if (UserIn != null)
            //    {
            //        outcome.user_id = UserIn.id;
            //    }

            //}
            db.Entry(expencess).State = EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "تم تعديل المصروف بنجاح ";

            return RedirectToAction("ShowExpencess");
        }


    }
}