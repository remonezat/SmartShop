using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{

    public class EmployeesController : BaseController
    {
        SmartShopEntities1 db = new SmartShopEntities1();

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
        [HttpPost]
        public ActionResult EmployeeDiscount(EmployeeDiscount employeeDiscount)
        {
            db.EmployeeDiscounts.Add(employeeDiscount);
            db.SaveChanges();
            TempData["SuccessMessage"] = " تم الحفظ !! ";

            return RedirectToAction("EmployeeDiscount");
        }

        public ActionResult ShowDiscounts()
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

        public JsonResult GetEmployeesDiscounts(DateTime DateF, DateTime DateT, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var SelectEmpDiscounts = db.EmployeeDiscounts.Where(x => x.Date >= DateF && x.Date <= DateT).Select(x => new { x.Id, x.Date, x.Employee.EName, x.Amount, x.Note, x.EmpId }).ToList();

            if (ID > 0)
            {
                SelectEmpDiscounts = SelectEmpDiscounts.Where(x => x.EmpId == ID).ToList();

            }
            return Json(SelectEmpDiscounts, JsonRequestBehavior.AllowGet);


        }
        public JsonResult DeleteDiscount(DateTime DateF, DateTime DateT, int EmpID = 0, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;


            if (ID > 0)
            {
                var selectemloyeeDiscount = db.EmployeeDiscounts.Where(x => x.Id == ID).FirstOrDefault();
                db.EmployeeDiscounts.Remove(selectemloyeeDiscount);
                db.SaveChanges();

            }
            var SelectEmpDiscounts = db.EmployeeDiscounts.Where(x => x.Date >= DateF && x.Date <= DateT).Select(x => new { x.Id, x.Date, x.Employee.EName, x.Amount, x.Note, x.EmpId }).ToList();

            if (EmpID > 0)
            {
                SelectEmpDiscounts = SelectEmpDiscounts.Where(x => x.EmpId == ID).ToList();

            }

            var data = new { result1 = SelectEmpDiscounts, message1 = "تم حذف الخصم" };

            return Json(data, JsonRequestBehavior.AllowGet);



        }


        public ActionResult EditDiscount(int Id =0)
        {
            var SelectDiscount = db.EmployeeDiscounts.Where(x => x.Id == Id).FirstOrDefault();
            var date = SelectDiscount.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ViewData["Dt"] = date;
            ViewBag.Employees = db.Employees.ToList();

            return View(SelectDiscount);
        }
      [HttpPost]
        public ActionResult EditDiscount(EmployeeDiscount  employeeDiscount)
        {

            db.Entry(employeeDiscount).State = EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "تم تعديل  بنجاح ";
            return RedirectToAction("ShowDiscounts");
        }



        public ActionResult PaySalary()
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
        public JsonResult GetEmpWithdraw(DateTime Date,int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID > 0)
            {
                var EmpWithdraw = db.GetEmpWithdraw(ID,Date).ToList();
                return Json(EmpWithdraw, JsonRequestBehavior.AllowGet);


            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmpDescount(DateTime Date, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID > 0)
            {
                var EmpDescount = db.GetEmpDescount(ID, Date).ToList();
                return Json(EmpDescount, JsonRequestBehavior.AllowGet);


            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PaySalary(EmployeePayedSalary payedSalary)
        {
            var SelectIsPaied = db.CheckEmpPay(payedSalary.EmpId, payedSalary.Date).FirstOrDefault();
            if (SelectIsPaied !=null)
            {
                TempData["DeleteMessage"] = " تم سداد قبض هذا الشهر لهذا الموظف من قبل ";

                return RedirectToAction("PaySalary");
            }

            if (payedSalary.EmpId ==null)
            {
                TempData["DeleteMessage"] = " اختر الموظف اولا  !! ";

                return RedirectToAction("PaySalary");

            }
            if (payedSalary.Amount <=0)
            {
                TempData["DeleteMessage"] = "لا يوجد رصيد للموظف ";

                return RedirectToAction("PaySalary");
            }
            db.EmployeePayedSalaries.Add(payedSalary);
            db.SaveChanges();
            TempData["SuccessMessage"] = " تم السداد !! ";

            return RedirectToAction("PaySalary");
        }
        }
}