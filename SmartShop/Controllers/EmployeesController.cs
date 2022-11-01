using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{

    public class EmployeesController : Controller
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: Employees
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
        public ActionResult Add(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            TempData["SuccessMessage"] = " تم الحفظ !! ";

            return RedirectToAction("Add");
        }


        public ActionResult ShowEmployees()
        {
            ViewBag.SuccessMessage = "Empty";
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            ViewBag.Employees = db.Employees.ToList();
            return View();
        }
        public JsonResult GetEmployees(string EmpName = null)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var rest = db.Employees.ToList();
          

            if (EmpName != "--الكل--")
            {
                rest = rest.Where(x => x.EName == EmpName).ToList();
            }
            
            return Json(rest, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Edit(int Id=0)
        {
            var SelectEmployee = db.Employees.Where(x => x.Id == Id).FirstOrDefault();

            return View(SelectEmployee);
        }
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "تم تعديل  بنجاح ";


            return RedirectToAction("ShowEmployees");
        }



     
        public JsonResult DeleteEmployee(string EmpName = null, int EmpId = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //bool isdeleted = false;
            var rest = db.Employees.ToList();
            var data = new { result1 = rest, message1 = "لا يمكن حذف هذا الموظف" };


            if (EmpId > 0)
            {
                var selectEmpWithdraw = db.EmployeesWithdraws.Where(x => x.EmpId == EmpId).ToList();
                if (selectEmpWithdraw.Count == 0 )
                {
                    var selectEmployee = db.Employees.Where(x => x.Id == EmpId).FirstOrDefault();
                    db.Employees.Remove(selectEmployee);
                    db.SaveChanges();
                    rest = db.Employees.Where(x => x.Id != EmpId).ToList();

                }
                else
                {
                    if (EmpName != "--الكل--")
                    {
                        rest = rest.Where(x => x.EName == EmpName).ToList();
                    }
                    data = new { result1 = rest, message1 = "لا يمكن حذف هذا الصنف" };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }
            if (EmpName != "--الكل--")
            {
                rest = rest.Where(x => x.EName == EmpName).ToList();
            }
            data = new { result1 = rest, message1 = "تم حذف الموظف" };
            return Json(data, JsonRequestBehavior.AllowGet);



        }

        public ActionResult EmployeeDiscount()
        {

            return View();
        }


       

    }
}