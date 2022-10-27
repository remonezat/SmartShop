using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class PaymentsController : Controller
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: Payments
        public ActionResult PaymentSupplier()
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
            ViewBag.Accounts = db.Accounts.Where(x => x.AccType == "s" || x.AccType == "b").ToList();

            return View();
        }
        [HttpPost]
        public ActionResult PaymentSupplier(SupplierPayment supplierPayment)
        {
            db.SupplierPayments.Add(supplierPayment);
            db.SaveChanges();
            TempData["SuccessMessage"] = " تم السداد بنجاح !! ";

            return RedirectToAction("PaymentSupplier");

        }
        public ActionResult PaymentCustomer()
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
            ViewBag.Accounts = db.Accounts.Where(x => x.AccType == "c" || x.AccType == "b").ToList();

            return View();
        }
        [HttpPost]
        public ActionResult PaymentCustomer(CustomerPayment customerPayment)
        {
            db.CustomerPayments.Add(customerPayment);
            db.SaveChanges();
            TempData["SuccessMessage"] = " تم تحصيل بنجاح !! ";

            return RedirectToAction("PaymentCustomer");

        }

        public JsonResult GetPersonCredit(int AccID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (AccID > 0)
            {
                if (AccID != 0)
                {
                    var AccCredit = db.Get_PersonAccountCredit(AccID);
                    return Json(AccCredit, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    var AccCredit = db.Get_PersonAccountCredit(AccID);

                    return Json(AccCredit, JsonRequestBehavior.AllowGet);

                }
            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AccountStatment()
        {
            ViewBag.Accounts = db.Accounts.ToList();

            return View();
        }

        public JsonResult GetAccountCredit(DateTime DateF, DateTime DateT, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID > 0)
            {
                    var AccountStatment = db.GetAccountStatement(ID, DateF, DateT).ToList();
                    var AccCredit = db.Get_PersonAccountCredit_before(ID, DateF).FirstOrDefault();
                    float Amount = 0;

                    foreach (var item in AccountStatment)
                    {

                        if (Amount == 0)
                        {
                            if (item.creditActions >= item.debitActions)
                            {
                                item.balanceAfter = (AccCredit.final + item.creditActions).ToString();
                            }
                            else
                            {
                                item.balanceAfter = (AccCredit.final - item.debitActions).ToString();
                            }
                            Amount = float.Parse(item.balanceAfter);
                            if (Amount >= 0)
                            {
                                item.balanceType = "دائن";
                            }
                            else
                            {
                                item.balanceType = "مدين";

                            }
                            item.balanceAfter = Math.Abs(float.Parse(item.balanceAfter)).ToString();
                        }
                        else
                        {
                            if (item.creditActions >= item.debitActions)
                            {
                                item.balanceAfter = (Amount + item.creditActions).ToString();
                            }
                            else
                            {
                                item.balanceAfter = (Amount - item.debitActions).ToString();
                            }
                            Amount = float.Parse(item.balanceAfter);
                            if (Amount >= 0)
                            {
                                item.balanceType = "دائن";
                            }
                            else
                            {
                                item.balanceType = "مدين";

                            }
                            item.balanceAfter = Math.Abs(float.Parse(item.balanceAfter)).ToString();

                        }


                    }




                    return Json(AccountStatment, JsonRequestBehavior.AllowGet);


                
            
            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccountCreditBefore(DateTime DateF, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID > 0)
            {

                var AccCredit = db.Get_PersonAccountCredit_before(ID, DateF).ToList();
                return Json(AccCredit, JsonRequestBehavior.AllowGet);




            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }

    }
}