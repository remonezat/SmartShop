using Newtonsoft.Json;
using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class EmployeeWithdrawController : Controller
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: EmployeeWithdraw
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
            ViewBag.Employees = db.Employees.ToList();
            return View();
        }
        public JsonResult GetEmpSalary(int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID > 0)
            {
                var EmpSalary = db.Employees.Where(x => x.Id == ID).ToList();
                return Json(EmpSalary, JsonRequestBehavior.AllowGet);


            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Add(EmployeesWithdraw employeesWithdraw)
        {
           

            db.EmployeesWithdraws.Add(employeesWithdraw);
            db.SaveChanges();
            TempData["SuccessMessage"] = " تم الحفظ !! ";

            return RedirectToAction("Add");
        }




        public ActionResult ShowWithdraws()
        {
            ViewBag.SuccessMessage = "Empty";
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            ViewBag.Employees = db.Employees.ToList();

            return View();
        }

        public JsonResult GetEmployeesWithdraws(DateTime DateF, DateTime DateT, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var SelectEmpWithdraw = db.EmployeesWithdraws.Where(x => x.Date >= DateF && x.Date <= DateT).Select(x => new { x.Id, x.Date, x.Employee.EName, x.Amount,x.Note ,x.EmpId}).ToList();

             if (ID > 0)
            {
                SelectEmpWithdraw = SelectEmpWithdraw.Where(x => x.EmpId == ID).ToList();

            }
            return Json(SelectEmpWithdraw, JsonRequestBehavior.AllowGet);


        }


  
        public JsonResult Delete(DateTime DateF, DateTime DateT, int EmpID = 0, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
          

            if (ID > 0)
            {
                var selectemloyeewithdraw = db.EmployeesWithdraws.Where(x => x.Id == ID).FirstOrDefault();
                db.EmployeesWithdraws.Remove(selectemloyeewithdraw);
                db.SaveChanges();

            }
            var SelectEmpWithdraw = db.EmployeesWithdraws.Where(x => x.Date >= DateF && x.Date <= DateT).Select(x => new { x.Id, x.Date, x.Employee.EName, x.Amount, x.Note, x.EmpId }).ToList();

            if (EmpID > 0)
            {
                SelectEmpWithdraw = SelectEmpWithdraw.Where(x => x.EmpId == ID).ToList();

            }

            var data = new { result1 = SelectEmpWithdraw, message1 = "تم حذف السحب" };

            return Json(data, JsonRequestBehavior.AllowGet);



        }

    }
}