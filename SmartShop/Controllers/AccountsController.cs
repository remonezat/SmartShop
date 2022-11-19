using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class AccountsController : BaseController
    {
        SmartShopEntities1 db = new SmartShopEntities1();

        // GET: Accounts
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
        public ActionResult Add(Account account)
        {
            var SelectAccounts = db.Accounts.Where(x => x.AccName == account.AccName).FirstOrDefault();
            if (SelectAccounts==null)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                TempData["SuccessMessage"] = " تم الحفظ !! ";
            }
            else
            {
                TempData["DeleteMessage"] = "الاسم موجودة بالفعل !!";
            }
            return RedirectToAction("Add");
        }
        public ActionResult ShowAccounts()
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
            ViewBag.Accounts = db.Accounts.ToList();

            return View();
        }

        public JsonResult GetAccounts( int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var SelectAccounts = db.Accounts.ToList();
            foreach (var item in SelectAccounts)
            {
                if (item.AccType=="c")
                {
                    item.AccType = "عميل";
                }
                else if (item.AccType == "s")
                {
                    item.AccType = "مورد";

                }
                else if (item.AccType == "b")
                {
                    item.AccType = "عميل و مورد";

                }
            }
            if (ID > 0)
            {
                SelectAccounts = SelectAccounts.Where(x => x.Id == ID).ToList();

            }
            return Json(SelectAccounts, JsonRequestBehavior.AllowGet);


        }


        public JsonResult Delete(int AcId = 0, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var SelectAccounts = db.Accounts.ToList();
         
            if (ID > 0)
            {
                SelectAccounts = SelectAccounts.Where(x => x.Id == ID).ToList();

            }
            var data = new { result1 = SelectAccounts, message1 = "تم حذف الحساب" };

            var SelectSales = db.Sales.Where(x => x.AccId == AcId).FirstOrDefault();
            var SelectPurchases = db.Purchases.Where(x => x.AccId == AcId).FirstOrDefault();
            var SelectCustomerPayments = db.CustomerPayments.Where(x => x.AccId == AcId).FirstOrDefault();
            var SelectSupplierPayments = db.SupplierPayments.Where(x => x.AccId == AcId).FirstOrDefault();
            if (SelectSales==null&& SelectPurchases==null&& SelectCustomerPayments==null&& SelectSupplierPayments==null)
            {

                    var Account = db.Accounts.Where(x => x.Id == AcId).FirstOrDefault();
                    db.Accounts.Remove(Account);
                    db.SaveChanges();

                foreach (var item in SelectAccounts)
                {
                    if (item.AccType == "c")
                    {
                        item.AccType = "عميل";
                    }
                    else if (item.AccType == "s")
                    {
                        item.AccType = "مورد";

                    }
                    else if (item.AccType == "b")
                    {
                        item.AccType = "عميل و مورد";

                    }
                }
                data = new { result1 = SelectAccounts.Where(x => x.Id != AcId).ToList(), message1 = " تم الحذف !!" };

            }

            foreach (var item in SelectAccounts)
            {
                if (item.AccType == "c")
                {
                    item.AccType = "عميل";
                }
                else if (item.AccType == "s")
                {
                    item.AccType = "مورد";

                }
                else if (item.AccType == "b")
                {
                    item.AccType = "عميل و مورد";

                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);


            



        }

        public ActionResult Edit(int Id)
        {
            var selectAccount = db.Accounts.Where(x => x.Id == Id).FirstOrDefault();
            if (selectAccount.AccType=="c")
            {
                ViewBag.customer = "checked";
            }
            else if (selectAccount.AccType == "s")
            {
                ViewBag.supplier = "checked";
            }
            else
            {
                ViewBag.both = "checked";

            }
            return View(selectAccount);
        }
        [HttpPost]
        public ActionResult Edit(Account account)
        {
          
                db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = " تم التعديل بنجاح !! ";

            return RedirectToAction("ShowAccounts");
        }
    }
}