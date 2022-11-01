using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class ShiftsController : Controller
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: Shifts
        public ActionResult Create()
        {
            var SelectOpenedShifts = db.Shifts.OrderByDescending(x => x.Id).Where(x => x.IsCloses == false).FirstOrDefault();
            if (SelectOpenedShifts != null)
            {
                ViewBag.Message = "يوجد وردية مفتوحة بالفعل";
                ViewBag.disable = "disabled";
                ViewBag.Name = "اغلاق الوردية";
            }
            else
            {
                ViewBag.hidden = "hidden";
                ViewBag.Name = "فتح الوردية ";


            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(Shift shift)
        {
            var SelectOpenedShifts = db.Shifts.OrderByDescending(x => x.Id).Where(x => x.IsCloses == false).FirstOrDefault();
            if (SelectOpenedShifts==null)
            {
                shift.IsCloses = false;
                shift.Useropen = 1;

                db.Shifts.Add(shift);
                db.SaveChanges();
            }
            else
            {
                var OpenedShift = db.Shifts.Find(SelectOpenedShifts.Id);
                OpenedShift.UserClose = 1;

                OpenedShift.DateClose = DateTime.Now;
                OpenedShift.IsCloses = true;
                OpenedShift.AmountClose = shift.AmountClose;
                db.SaveChanges();
            }
            
            return RedirectToAction("Create");
        }
      

    }
}