using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Bson;
using SmartShop.Models;
namespace SmartShop.Controllers
{
    public class ItemsController : Controller
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: Items
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
            ViewBag.Categories=db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Item item, HttpPostedFileBase img1)
        {
            var SelectCurrentItems = db.Items.Where(x => x.ItemName == item.ItemName).FirstOrDefault();
            if (SelectCurrentItems == null)
            {
                if (img1 != null)
                {
                    item.Img = new byte[img1.ContentLength];
                    img1.InputStream.Read(item.Img, 0, img1.ContentLength);

                }
                db.Items.Add(item);
                db.SaveChanges();
                TempData["SuccessMessage"] = "تم الحفظ نجاح !!";
            }
            else
            {
                TempData["DeleteMessage"] = "هذا الصنف موجود بالفعل";
            }

            return RedirectToAction("Add");
        }


        public ActionResult ShowItems()
        {
            ViewBag.SuccessMessage = "Empty";
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Companies = db.Items.DistinctBy(x => x.Company).ToList();


            return View();
        }
        public JsonResult Getitems(string Name = null)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (Name != null)
            {
                List<Item> rest = db.Items.Where(x => x.ItemName.Contains(Name)).ToList();

                return Json(rest, JsonRequestBehavior.AllowGet);
            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetitemDetails(int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID != 0)
            {

                List<Item> rest = db.Items.Where(x => x.Id == ID).ToList();

                return Json(rest, JsonRequestBehavior.AllowGet);
            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getitemsbybarcode(string Name = null)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (Name != null)
            {
                List<Item> rest = db.Items.Where(x => x.Barcode.Contains(Name)).ToList();

                return Json(rest, JsonRequestBehavior.AllowGet);
            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemData(string Name = null, string Barcode = null, string CatName = null, string Company = null)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var rest = db.Items.Select(x => new { x.Id, x.ItemName, x.Barcode, x.PricePur, x.PriceSell, x.Category.CatName, x.Company }).ToList();
            if (Name != "")
            {
                rest = rest.Where(x => x.ItemName == Name).ToList();
            }
           
            if (CatName != "--الكل--")
            {
                rest = rest.Where(x => x.CatName == CatName).ToList();
            }
            if (Company != "--الكل--")
            {
                rest = rest.Where(x => x.Company == Company).ToList();
            }
            if (Barcode != "")
            {
                rest = rest.Where(x => x.Barcode==Barcode).ToList();
            }
            return Json(rest, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteItem(string Name = null, string Barcode = null, string CatName = null, string Company = null,int ID=0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //bool isdeleted = false;
            var rest = db.Items.Select(x => new { x.Id, x.ItemName, x.Barcode, x.PricePur, x.PriceSell, x.Category.CatName, x.Company }).ToList();
            var data = new { result1 = rest, message1 = "لا يمكن حذف هذا الصنف" };


            if (ID > 0)
            {
                var selectSalesdet = db.SalesDetails.Where(x => x.ItemId == ID).ToList();
                var selectpurdet = db.PurchaseDetails.Where(x => x.ItemId == ID).ToList();
                if (selectSalesdet.Count == 0 && selectpurdet.Count == 0)
                {
                    var Selectitem = db.Items.Where(x => x.Id == ID).FirstOrDefault();
           
                    db.Items.Remove(Selectitem);
                    db.SaveChanges();
                    rest = db.Items.Where(x=>x.Id!=ID).Select(x => new { x.Id, x.ItemName, x.Barcode, x.PricePur, x.PriceSell, x.Category.CatName, x.Company }).ToList();

                }
                else
                {
                    if (Name != "")
                    {
                        rest = rest.Where(x => x.ItemName == Name).ToList();
                    }

                    if (CatName != "--الكل--")
                    {
                        rest = rest.Where(x => x.CatName == CatName).ToList();
                    }
                    if (Company != "--الكل--")
                    {
                        rest = rest.Where(x => x.Company == Company).ToList();
                    }
                    if (Barcode != "")
                    {
                        rest = rest.Where(x => x.Barcode == Barcode).ToList();
                    }
                    data = new { result1 = rest, message1 = "لا يمكن حذف هذا الصنف" };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }
            if (Name != "")
            {
                rest = rest.Where(x => x.ItemName == Name).ToList();
            }

            if (CatName != "--الكل--")
            {
                rest = rest.Where(x => x.CatName == CatName).ToList();
            }
            if (Company != "--الكل--")
            {
                rest = rest.Where(x => x.Company == Company).ToList();
            }
            if (Barcode != "")
            {
                rest = rest.Where(x => x.Barcode == Barcode).ToList();
            }
            data = new { result1 = rest, message1 = "تم حذف الصنف" };
            return Json(data, JsonRequestBehavior.AllowGet);



        }

        public ActionResult Edit(int Id)
        {
            var SelectItem = db.Items.Where(x => x.Id == Id).FirstOrDefault();
            ViewBag.Categories=db.Categories.ToList();

            return View(SelectItem);

        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpeg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Items where temp.Id == Id select temp.Img;
            byte[] cover = q.FirstOrDefault();
            return cover;
        }
        [HttpPost]
        public ActionResult Edit(Item item, HttpPostedFileBase img1)
        {
           
                if (img1 != null)
                {
                    item.Img = new byte[img1.ContentLength];
                    img1.InputStream.Read(item.Img, 0, img1.ContentLength);

                }
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "تم تعديل نجاح !!";
            
           

            return RedirectToAction("ShowItems");

        }


    }
}