using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class UsersController : Controller
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: Users
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
        public ActionResult Add( User user,FormCollection formCollection)
        {
            bool s = false;
            bool sre = false;
            bool pur = false;
            bool purre = false;
            bool editbills = false;

            bool ex = false;
            bool editex = false;


            bool addemp = false;
            bool withdraw = false;
            bool salary = false;
            bool perm = false;
            bool editemp = false;
            bool editwithdraw = false;
            bool salarydis = false;
            bool editperm = false;



            bool addacc = false;
            bool editacc = false;
            bool paycus = false;
            bool paysup = false;
            bool accstatment = false;

            bool cat = false;
            bool additem = false;
            bool edititem = false;
            bool addstore = false;

            bool trans = false;
            bool edittrans = false;

            if (!string.IsNullOrEmpty(formCollection["s"])) { s = true; }
            if (!string.IsNullOrEmpty(formCollection["sre"])) { sre = true; }
            if (!string.IsNullOrEmpty(formCollection["pur"])) { pur = true; }
            if (!string.IsNullOrEmpty(formCollection["purre"])) { purre = true; }
            if (!string.IsNullOrEmpty(formCollection["editbills"])) { editbills = true; }


            if (!string.IsNullOrEmpty(formCollection["ex"])) { ex = true; }
            if (!string.IsNullOrEmpty(formCollection["editex"])) { editex = true; }

            if (!string.IsNullOrEmpty(formCollection["addemp"])) { addemp = true; }
            if (!string.IsNullOrEmpty(formCollection["withdraw"])) { withdraw = true; }
            if (!string.IsNullOrEmpty(formCollection["salary"])) { salary = true; }
            if (!string.IsNullOrEmpty(formCollection["perm"])) { perm = true; }
            if (!string.IsNullOrEmpty(formCollection["editemp"])) { editemp = true; }
            if (!string.IsNullOrEmpty(formCollection["editwithdraw"])) { editwithdraw = true; }
            if (!string.IsNullOrEmpty(formCollection["salarydis"])) { salarydis = true; }
            if (!string.IsNullOrEmpty(formCollection["editperm"])) { editperm = true; }




            if (!string.IsNullOrEmpty(formCollection["addacc"])) { addacc = true; }
            if (!string.IsNullOrEmpty(formCollection["editacc"])) { editacc = true; }
            if (!string.IsNullOrEmpty(formCollection["paycus"])) { paycus = true; }
            if (!string.IsNullOrEmpty(formCollection["paysup"])) { paysup = true; }
            if (!string.IsNullOrEmpty(formCollection["accstatment"])) { accstatment = true; }


            if (!string.IsNullOrEmpty(formCollection["cat"])) { cat = true; }
            if (!string.IsNullOrEmpty(formCollection["additem"])) { additem = true; }
            if (!string.IsNullOrEmpty(formCollection["edititem"])) { edititem = true; }
            if (!string.IsNullOrEmpty(formCollection["addstore"])) { addstore = true; }

            if (!string.IsNullOrEmpty(formCollection["trans"])) { trans = true; }
            if (!string.IsNullOrEmpty(formCollection["edittrans"])) { edittrans = true; }

            string d ="";
            // bills
            if (s==true)
            {
                d = "s,";
            }
            if (sre == true)
            {
                d += "sre,";
            }
            if (pur == true)
            {
                d += "pur,";
            }
            if (purre == true)
            {
                d += "purre,";
            }
            if (editbills == true)
            {
                d += "editbills,";
            }
            // expences

            if (ex == true)
            {
                d += "ex,";
            }
            if (editex == true)
            {
                d += "editex,";
            }



            //emp
            if (addemp == true)
            {
                d += "addemp,";
            }
            if (editemp == true)
            {
                d += "editemp,";
            }
            if (salary == true)
            {
                d += "salary,";
            }
            if (withdraw == true)
            {
                d += "withdraw,";
            }
            if (editwithdraw == true)
            {
                d += "editwithdraw,";
            }
            if (perm == true)
            {
                d += "perm,";
            }
            if (editperm == true)
            {
                d += "editperm,";
            }
            if (salarydis == true)
            {
                d += "salarydis,";
            }



            //accounts
            if (addacc == true)
            {
                d += "addacc,";
            }
            if (editacc == true)
            {
                d += "editacc,";
            }
            if (paycus == true)
            {
                d += "paycus,";
            }

            if (paysup == true)
            {
                d += "paysup,";
            }
            if (accstatment == true)
            {
                d += "accstatment,";
            }
            // items
            if (additem == true)
            {
                d += "additem,";
            }
            if (edititem == true)
            {
                d += "edititem,";
            }

            if (cat == true)
            {
                d += "cat,";
            }
            if (addstore == true)
            {
                d += "addstore,";
            }

            //trans
            if (trans == true)
            {
                d += "trans,";
            }
            if (edittrans == true)
            {
                d += "edittrans,";
            }

            //reports
            user.UserScreens = d;
            db.Users.Add(user);
            db.SaveChanges();
            TempData["SuccessMessage"] = "تم الحفظ نجاح !!";

            return RedirectToAction("Add");
        }

        public ActionResult ShowUsers ()
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
            ViewBag.Users = db.Users.ToList();
            return View();
        }
        public JsonResult GetUsers(string EmpName = null)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var rest = db.Users.ToList();


            if (EmpName != "--الكل--")
            {
                rest = rest.Where(x => x.UserName == EmpName).ToList();
            }

            return Json(rest, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Delete(string EmpName = null, int EmpId = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //bool isdeleted = false;
            var rest = db.Users.ToList();
            var data = new { result1 = rest, message1 = "لا يمكن حذف هذا المستخدم" };


            if (EmpId > 0)
            {
                
                    var selectUser = db.Users.Where(x => x.Id == EmpId).FirstOrDefault();
                    db.Users.Remove(selectUser);
                    db.SaveChanges();
                    rest = db.Users.Where(x => x.Id != EmpId).ToList();


            }
            if (EmpName != "--الكل--")
            {
                rest = rest.Where(x => x.UserName == EmpName).ToList();
            }
            data = new { result1 = rest, message1 = "تم حذف المستخدم" };
            return Json(data, JsonRequestBehavior.AllowGet);



        }


        public ActionResult Edit(int Id)
        {
            var SelectUser = db.Users.Where(x => x.Id == Id).FirstOrDefault();

            string[] strArray = SelectUser.UserScreens.Split(',');
            foreach (var item in strArray)
            {
                if (item == "s")
                {
                    ViewBag.S = "checked";
                }
                if (item == "sre")
                {
                    ViewBag.sre = "checked";
                }
                if (item=="pur")
                {
                    ViewBag.pur = "checked";
                }
                if (item=="purre")
                {
                    ViewBag.purre = "checked";
                }
                if (item=="editbills")
                {
                    ViewBag.editbills = "checked";
                }
                // expences

                if (item=="ex")
                {
                    ViewBag.ex = "checked";
                }
                if (item=="editex")
                {
                    ViewBag.editex = "checked";
                }



                //emp
                if (item=="addemp")
                {
                    ViewBag.addemp = "checked";
                }
                if (item=="editemp")
                {
                    ViewBag.editemp = "checked";
                }
                if (item=="salary")
                {
                    ViewBag.salary = "checked";
                }
                if (item=="withdraw")
                {
                    ViewBag.withdraw = "checked";
                }
                if (item=="editwithdraw")
                {
                    ViewBag.editwithdraw = "checked";
                }
                if (item=="perm")
                {
                    ViewBag.perm = "checked";
                }
                if (item== "editperm")
                {
                    ViewBag.editperm = "checked";
                }
                if (item=="salarydis")
                {
                    ViewBag.salarydis = "checked";
                }



                //accounts
                if (item=="addacc")
                {
                    ViewBag.addacc = "checked";
                }
                if (item=="editacc")
                {
                    ViewBag.editacc = "checked";
                }
                if (item=="paycus")
                {
                    ViewBag.paycus = "checked";
                }

                if (item=="paysup")
                {
                    ViewBag.paysup = "checked";
                }
                if (item=="accstatment")
                {
                    ViewBag.accstatment = "checked";
                }
                // items
                if (item=="additem")
                {
                    ViewBag.additem = "checked";
                }
                if (item=="edititem")
                {
                    ViewBag.edititem = "checked";
                }

                if (item=="cat")
                {
                    ViewBag.cat = "checked";
                }
                if (item=="addstore")
                {
                    ViewBag.addstore = "checked";
                }

                //trans
                if (item=="trans")
                {
                    ViewBag.trans = "checked";
                }
                if (item=="edittrans")
                {
                    ViewBag.edittrans = "checked";
                }
            }
            //reports
            return View(SelectUser);
        }

        [HttpPost]
        public ActionResult Edit(User user,FormCollection formCollection)
        {

            bool s = false;
            bool sre = false;
            bool pur = false;
            bool purre = false;
            bool editbills = false;

            bool ex = false;
            bool editex = false;


            bool addemp = false;
            bool withdraw = false;
            bool salary = false;
            bool perm = false;
            bool editemp = false;
            bool editwithdraw = false;
            bool salarydis = false;
            bool editperm = false;



            bool addacc = false;
            bool editacc = false;
            bool paycus = false;
            bool paysup = false;
            bool accstatment = false;

            bool cat = false;
            bool additem = false;
            bool edititem = false;
            bool addstore = false;

            bool trans = false;
            bool edittrans = false;

            if (!string.IsNullOrEmpty(formCollection["s"])) { s = true; }
            if (!string.IsNullOrEmpty(formCollection["sre"])) { sre = true; }
            if (!string.IsNullOrEmpty(formCollection["pur"])) { pur = true; }
            if (!string.IsNullOrEmpty(formCollection["purre"])) { purre = true; }
            if (!string.IsNullOrEmpty(formCollection["editbills"])) { editbills = true; }


            if (!string.IsNullOrEmpty(formCollection["ex"])) { ex = true; }
            if (!string.IsNullOrEmpty(formCollection["editex"])) { editex = true; }

            if (!string.IsNullOrEmpty(formCollection["addemp"])) { addemp = true; }
            if (!string.IsNullOrEmpty(formCollection["withdraw"])) { withdraw = true; }
            if (!string.IsNullOrEmpty(formCollection["salary"])) { salary = true; }
            if (!string.IsNullOrEmpty(formCollection["perm"])) { perm = true; }
            if (!string.IsNullOrEmpty(formCollection["editemp"])) { editemp = true; }
            if (!string.IsNullOrEmpty(formCollection["editwithdraw"])) { editwithdraw = true; }
            if (!string.IsNullOrEmpty(formCollection["salarydis"])) { salarydis = true; }
            if (!string.IsNullOrEmpty(formCollection["editperm"])) { editperm = true; }




            if (!string.IsNullOrEmpty(formCollection["addacc"])) { addacc = true; }
            if (!string.IsNullOrEmpty(formCollection["editacc"])) { editacc = true; }
            if (!string.IsNullOrEmpty(formCollection["paycus"])) { paycus = true; }
            if (!string.IsNullOrEmpty(formCollection["paysup"])) { paysup = true; }
            if (!string.IsNullOrEmpty(formCollection["accstatment"])) { accstatment = true; }


            if (!string.IsNullOrEmpty(formCollection["cat"])) { cat = true; }
            if (!string.IsNullOrEmpty(formCollection["additem"])) { additem = true; }
            if (!string.IsNullOrEmpty(formCollection["edititem"])) { edititem = true; }
            if (!string.IsNullOrEmpty(formCollection["addstore"])) { addstore = true; }

            if (!string.IsNullOrEmpty(formCollection["trans"])) { trans = true; }
            if (!string.IsNullOrEmpty(formCollection["edittrans"])) { edittrans = true; }

            string d = "";
            // bills
            if (s == true)
            {
                d = "s,";
            }
            if (sre == true)
            {
                d += "sre,";
            }
            if (pur == true)
            {
                d += "pur,";
            }
            if (purre == true)
            {
                d += "purre,";
            }
            if (editbills == true)
            {
                d += "editbills,";
            }
            // expences

            if (ex == true)
            {
                d += "ex,";
            }
            if (editex == true)
            {
                d += "editex,";
            }



            //emp
            if (addemp == true)
            {
                d += "addemp,";
            }
            if (editemp == true)
            {
                d += "editemp,";
            }
            if (salary == true)
            {
                d += "salary,";
            }
            if (withdraw == true)
            {
                d += "withdraw,";
            }
            if (editwithdraw == true)
            {
                d += "editwithdraw,";
            }
            if (perm == true)
            {
                d += "perm,";
            }
            if (editperm == true)
            {
                d += "editperm,";
            }
            if (salarydis == true)
            {
                d += "salarydis,";
            }



            //accounts
            if (addacc == true)
            {
                d += "addacc,";
            }
            if (editacc == true)
            {
                d += "editacc,";
            }
            if (paycus == true)
            {
                d += "paycus,";
            }

            if (paysup == true)
            {
                d += "paysup,";
            }
            if (accstatment == true)
            {
                d += "accstatment,";
            }
            // items
            if (additem == true)
            {
                d += "additem,";
            }
            if (edititem == true)
            {
                d += "edititem,";
            }

            if (cat == true)
            {
                d += "cat,";
            }
            if (addstore == true)
            {
                d += "addstore,";
            }

            //trans
            if (trans == true)
            {
                d += "trans,";
            }
            if (edittrans == true)
            {
                d += "edittrans,";
            }

            //reports
            user.UserScreens = d;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = " تم التعديل بنجاح !! ";

            return RedirectToAction("ShowUsers");
        }




    }
}