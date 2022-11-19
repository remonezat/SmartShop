using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SmartShop.Models;
namespace SmartShop.Controllers
{
    public class StoresController : BaseController
    {
        SmartShopEntities1 db = new SmartShopEntities1();
        // GET: Stores
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
            ViewBag.Stores = db.Stores.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Store store)
        {
            var SelectCurrentStores = db.Stores.Where(x=>x.StoreName==store.StoreName).FirstOrDefault();
            if (SelectCurrentStores == null)
            {
                db.Stores.Add(store);
                db.SaveChanges();
                TempData["SuccessMessage"] = " تم الحفظ !! ";
            }
            else
            {
                TempData["DeleteMessage"] = "المخزن موجودة بالفعل !!";
            }
            return RedirectToAction("Add");
        }

        public ActionResult Edit(int Id)
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
            var SelectStore = db.Stores.Where(x => x.Id == Id).FirstOrDefault();

            return View(SelectStore);
        }
        [HttpPost]
        public ActionResult Edit(Store store)
        {
            var SelectCurrentStores = db.Stores.Where(x => x.StoreName == store.StoreName).FirstOrDefault();

            if (SelectCurrentStores == null)
            {
                db.Entry(store).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = " تم التعديل بنجاح !! ";
                return RedirectToAction("Add");

            }
            else
            {
                TempData["DeleteMessage"] = "المخزن موجودة بالفعل !!";
                return RedirectToAction("Edit");

            }

        }

        public ActionResult Delete(int Id = 0)
        {
            if (Id > 0)
            {
                var SelectItemsstore = db.StoreTransfers.Where(x => x.StkFromId == Id|| x.StkToId == Id).FirstOrDefault();
                var SelectItemsSales = db.Sales.Where(x => x.StkId == Id).FirstOrDefault();
                var SelectItemsPurchase = db.Purchases.Where(x => x.StkId == Id).FirstOrDefault();
                var SelectItemsSalesRe = db.SalesRes.Where(x => x.StkId == Id).FirstOrDefault();
                var SelectItemsPurchaseRe = db.PurchaseRes.Where(x => x.StkId == Id).FirstOrDefault();


                if (SelectItemsstore == null&& SelectItemsSales==null&& SelectItemsPurchase==null&& SelectItemsSalesRe==null&& SelectItemsPurchaseRe==null)
                {
                    var SelectStore = db.Stores.Where(x => x.Id == Id).FirstOrDefault();
                    db.Stores.Remove(SelectStore);
                    TempData["SuccessMessage"] = " تم الحذف بنجاح !! ";

                    db.SaveChanges();
                }
                else
                {
                    TempData["DeleteMessage"] = "لا يمكن حذف هذا المخزن    !!";

                }

            }
            return RedirectToAction("Add");
        }

    }
}