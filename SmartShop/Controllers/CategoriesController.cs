using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShop.Models;
namespace SmartShop.Controllers
{
    public class CategoriesController : Controller
    {
        SmartShopEntities db = new SmartShopEntities();
        // GET: Categories
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
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category category)
        {
            var SelectCurrentCategories = db.Categories.Where(x => x.CatName == category.CatName).FirstOrDefault();
            if (SelectCurrentCategories==null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = " تم الحفظ !! ";
            }
            else
            {
                TempData["DeleteMessage"] = "الفئة موجودة بالفعل !!";
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
            var SelectCategory = db.Categories.Where(x => x.Id == Id).FirstOrDefault();

            return View(SelectCategory);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            var SelectCurrentCategories = db.Categories.Where(x => x.CatName == category.CatName).FirstOrDefault();

            if (SelectCurrentCategories == null)
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = " تم التعديل بنجاح !! ";
                return RedirectToAction("Add");

            }
            else
            {
                TempData["DeleteMessage"] = "الفئة موجودة بالفعل !!";
                return RedirectToAction("Edit");

            }

        }

        public ActionResult Delete(int Id =0)
        {
            if (Id>0)
            {
                var SelectItemsCat = db.Items.Where(x => x.CatId == Id).FirstOrDefault();
                if (SelectItemsCat==null)
                {
                    var SelectCategory = db.Categories.Where(x => x.Id == Id).FirstOrDefault();
                    db.Categories.Remove(SelectCategory);
                    TempData["SuccessMessage"] = " تم الحذف بنجاح !! ";

                    db.SaveChanges();
                }
                else
                {
                    TempData["DeleteMessage"] = "لا يمكن حذف هذه الفئة    !!";

                }

            }
            return RedirectToAction("Add");
        }


    }
}