using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class ShowBillsController : BaseController
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: ShowBills
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetBills(DateTime DateF, DateTime DateT, string BillTyp, string PayTyp)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (BillTyp != "0" && PayTyp == "0" )
            {
                var SelectBills = db.GetAllBills().Where(x => x.Date >= DateF && x.Date <= DateT && x.typ == BillTyp).ToList();
                return Json(SelectBills, JsonRequestBehavior.AllowGet);
            }
            else if (BillTyp == "0" && PayTyp != "0")
            {
                var SelectBills = db.GetAllBills().Where(x => x.Date >= DateF && x.Date <= DateT && x.n == PayTyp).ToList();
                return Json(SelectBills, JsonRequestBehavior.AllowGet);
            }
            else if (BillTyp != "0" && PayTyp != "0")
            {
                var SelectBills = db.GetAllBills().Where(x => x.Date >= DateF && x.Date <= DateT && x.n == PayTyp && x.typ == BillTyp).ToList();
                return Json(SelectBills, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var SelectBills = db.GetAllBills().Where(x => x.Date >= DateF && x.Date <= DateT).ToList();
                return Json(SelectBills, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult DeleteBill(DateTime DateF, DateTime DateT, string BillTyp, string PayTyp, int BillID, string BillType)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (BillType == "مبيعات")
            {
                var SelectSalesDetails = db.SalesDetails.Where(x => x.InvId == BillID).ToList();
                
                foreach (var item in SelectSalesDetails)
                {
                    db.SalesDetails.Remove(item);
                    db.SaveChanges();

                }
                var SelectSales = db.Sales.Where(x => x.Id == BillID).FirstOrDefault();
                db.Sales.Remove(SelectSales);
                db.SaveChanges();
            }
            else if (BillType == "مشتريات")
            {
                var SelectpurchaseDetails = db.PurchaseDetails.Where(x => x.InvId == BillID).ToList();
                foreach (var item in SelectpurchaseDetails)
                {
                    db.PurchaseDetails.Remove(item);
                    db.SaveChanges();
                }
                var SelectPurchase = db.Purchases.Where(x => x.Id == BillID).FirstOrDefault();
                db.Purchases.Remove(SelectPurchase);
                db.SaveChanges();
            }
            else if (BillType == "مرتجع بيع")
            {

                var SelectSalesReDetails = db.SalesReDetails.Where(x => x.InvId == BillID).ToList();
                foreach (var item in SelectSalesReDetails)
                {
                    db.SalesReDetails.Remove(item);
                    db.SaveChanges();

                }
                
                var SelectSalesRe = db.SalesRes.Where(x => x.Id == BillID).FirstOrDefault();
                db.SalesRes.Remove(SelectSalesRe);
                db.SaveChanges();
            }
            else if (BillType == "مرتجع شراء")
            {
                var SelectpurchaseReDetails = db.PurchaseReDetails.Where(x => x.InvId == BillID).ToList();
                foreach (var item in SelectpurchaseReDetails)
                {
                    db.PurchaseReDetails.Remove(item);
                    db.SaveChanges();
                }
                var SelectPurchaseRe = db.PurchaseRes.Where(x => x.Id == BillID).FirstOrDefault();
                db.PurchaseRes.Remove(SelectPurchaseRe);
                db.SaveChanges();
            }




            if (BillTyp != "0" && PayTyp == "0")
            {
                var SelectBills = db.GetAllBills().Where(x => x.Date >= DateF && x.Date <= DateT && x.typ == BillTyp).ToList();
                return Json(SelectBills, JsonRequestBehavior.AllowGet);
            }
            else if (BillTyp == "0" && PayTyp != "0")
            {
                var SelectBills = db.GetAllBills().Where(x => x.Date >= DateF && x.Date <= DateT && x.n == PayTyp).ToList();
                return Json(SelectBills, JsonRequestBehavior.AllowGet);
            }
            else if (BillTyp != "0" && PayTyp != "0")
            {
                var SelectBills = db.GetAllBills().Where(x => x.Date >= DateF && x.Date <= DateT && x.n == PayTyp && x.typ == BillTyp).ToList();
                return Json(SelectBills, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var SelectBills = db.GetAllBills().Where(x => x.Date >= DateF && x.Date <= DateT).ToList();
                return Json(SelectBills, JsonRequestBehavior.AllowGet);
            }
        }




    }
}