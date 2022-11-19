using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class StoreBillsController : BaseController
    {
        SmartShopEntities1 db = new SmartShopEntities1();

        // GET: StoreBills
        public ActionResult Index()
        {
            this.HttpContext.Response.AddHeader("refresh", "10; url=" + Url.Action("Index"));



            ViewBag.CurrentBills = db.dailybill().Where(x =>x.ISConfirm == null).ToList();


           
            ViewBag.ConfirmedBills = db.dailybill().Where(x => x.ISConfirm == true).ToList();





            return View();
        }


        public ActionResult ShowBill(int id)
        {
            var SelectBill = db.Sales.Where(x => x.Id == id).FirstOrDefault();
            var SelectBillDetails = db.SalesDetails.Where(x => x.InvId == id).ToList();
            ViewBag.SalesDetails = SelectBillDetails;

            var date = SelectBill.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ViewData["Dt"] = date;

            ViewBag.ItemCount = SelectBillDetails.Count();
            ViewBag.ItemsQuantity = SelectBillDetails.Sum(x => x.Count);

            if (SelectBill.IsCash == true)
            {
                ViewBag.PayOptionCash = "Checked";
            }
            else
            {
                ViewBag.PayoptionNoCash = "Checked";
            }
            if (SelectBill.StkId == 0)
            {
                ViewBag.stk = "Checked";
            }
            if (SelectBill.AccId !=0)
            {
                ViewBag.AccId = db.Accounts.Where(x =>x.Id == SelectBill.AccId).Select(x =>x.AccName).FirstOrDefault();

            }
            return View(SelectBill);
        }

        [HttpPost]
        public ActionResult ShowBill(Sale sale)
        {
            var SelectSales = db.Sales.Find(sale.Id);
            SelectSales.ISConfirm = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult ShowConfirmedBill(int id)
        {
            var SelectBill = db.Sales.Where(x => x.Id == id).FirstOrDefault();
            var SelectBillDetails = db.SalesDetails.Where(x => x.InvId == id).ToList();
            ViewBag.SalesDetails = SelectBillDetails;

            var date = SelectBill.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ViewData["Dt"] = date;

            ViewBag.ItemCount = SelectBillDetails.Count();
            ViewBag.ItemsQuantity = SelectBillDetails.Sum(x => x.Count);

            if (SelectBill.IsCash == true)
            {
                ViewBag.PayOptionCash = "Checked";
            }
            else
            {
                ViewBag.PayoptionNoCash = "Checked";
            }
            if (SelectBill.StkId == 0)
            {
                ViewBag.stk = "Checked";
            }
            if (SelectBill.AccId != 0)
            {
                ViewBag.AccId = db.Accounts.Where(x => x.Id == SelectBill.AccId).Select(x => x.AccName).FirstOrDefault();

            }

            return View(SelectBill);

        }
    }
}