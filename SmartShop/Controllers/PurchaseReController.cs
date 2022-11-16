﻿using Newtonsoft.Json;
using SmartShop.Models;
using SmartShop.PublicClasses;
using SmartShop.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartShop.Controllers
{
    public class PurchaseReController : BaseController
    {
        SmartShopEntities db = new SmartShopEntities();

        // GET: PurchaseRe
        public ActionResult Add()
        {
            ViewBag.Stores = db.Stores.ToList();

            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("PurchaseReBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            return View();
        }

        public JsonResult CreateBill(int ID = 0, string Name = null, float Price = 0, float Quantity = 0, float Total = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("PurchaseReBill");

            if (ID > 0 && Name != null && Price > 0 && Quantity > 0 && Total > 0)
            {
                float TBill = 0;
                int ICount = 0;
                float IQuantity = 0;



                if (cookie2 != null)
                {
                    var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                    if (!ProductsInCart.Any(x => x.Name == Name))
                    {
                        foreach (var item in ProductsInCart)
                        {
                            TBill += item.Total;
                            ICount += 1;
                            IQuantity += item.Quantity;
                        }
                        BillDetails billDetails = new BillDetails()
                        {
                            ID = ID,
                            Name = Name,
                            Price = Price,
                            Quantity = Quantity,
                            Total = Total,
                            FinalTotal = TBill + Total,
                            ItemsCount = ICount + 1,
                            TotalQuantity = IQuantity + Quantity

                        };
                        ProductsInCart.Add(billDetails);
                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("PurchaseReBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    BillDetails billDetails = new BillDetails()
                    {
                        ID = ID,
                        Name = Name,

                        Price = Price,
                        Quantity = Quantity,
                        Total = Total,
                        FinalTotal = Total,
                        ItemsCount = 1,
                        TotalQuantity = Quantity


                    };

                    List<BillDetails> ProductsInCart = new List<BillDetails>();
                    ProductsInCart.Add(billDetails);
                    var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                    var cookieText = Authentication.Encrypt(Serialize);
                    var authCookie = new HttpCookie("PurchaseReBill", cookieText);
                    authCookie.Expires = DateTime.Now.AddDays(30);
                    HttpContext.Response.Cookies.Add(authCookie);
                    return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                }


            }
            if (cookie2 != null)
            {
                var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

            }
            return Json(new List<BillDetails>(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemTotalPrice(int ID = 0, float ItemQ = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("PurchaseReBill");

            float TBill = 0;
            float IQuantity = 0;
            BillDetails billDetails = new BillDetails();
            if (ID > 0)
            {
                if (cookie2 != null)
                {
                    var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                    if (ProductsInCart.Any(x => x.ID == ID))
                    {


                        billDetails = ProductsInCart.Where(x => x.ID == ID).FirstOrDefault();


                        billDetails.Quantity = ItemQ;
                        billDetails.Total = ItemQ * billDetails.Price;
                        foreach (var item in ProductsInCart)
                        {
                            TBill += item.Total;
                            IQuantity += item.Quantity;
                        }
                        foreach (var item in ProductsInCart)
                        {
                            item.FinalTotal = TBill;
                            item.TotalQuantity = IQuantity;
                        }

                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("PurchaseReBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);


                    }



                    return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteItem(int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            float TBill = 0;

            float IQuantity = 0;
            if (ID > 0)
            {
                var cookie2 = HttpContext.Request.Cookies.Get("PurchaseReBill");
                if (cookie2 != null)
                {

                    var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));
                    if (ProductsInCart.Any(x => x.ID == ID))
                    {
                        int count = ProductsInCart.Count();
                        if (ProductsInCart.Count() == 1)
                        {

                            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
                        }
                        else
                        {
                            ProductsInCart.RemoveAll(y => y.ID == ID);
                            foreach (var item in ProductsInCart)
                            {
                                TBill += item.Total;
                                IQuantity += item.Quantity;

                            }
                            foreach (var item in ProductsInCart)
                            {
                                item.FinalTotal = TBill;
                                item.TotalQuantity = IQuantity;
                                item.ItemsCount = count - 1;
                            }

                        }

                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("PurchaseReBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Add(PurchaseRe purchase, PurchaseReDetail purchaseDetail, string isCash, FormCollection formCollection, float RPaied)
        {


            int? SelectShiftID = db.GetShift_id().FirstOrDefault();
            if (SelectShiftID == null || SelectShiftID == 0)
            {
                return Json(new { isValid = false, message = "برجاء فتح فترة عمل اولا" });

            }

            List<BillDetails> ProductsInCart = new List<BillDetails>();
            var cookie2 = HttpContext.Request.Cookies.Get("PurchaseReBill");
            if (cookie2 != null)
            {
                ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                if (ProductsInCart == null)
                {
                    return Json(new { isValid = false, message = " لا توجد اصناف بالفاتورة " });

                }

            }
            else
            {
                return Json(new { isValid = false, message = " لا توجد اصناف بالفاتورة " });

            }
            int UserId = 0;

            var cookie1 = HttpContext.Request.Cookies.Get("AdminInfo");
            if (cookie1 != null)
            {
                var UserIn = JsonConvert.DeserializeObject<User>(Authentication.Decrypt(cookie1.Value));
                if (UserIn != null)
                {
                    UserId = UserIn.Id;
                }

            }


            if (isCash == "0" && (purchase.AccId == null || purchase.AccId <= 0))
            {
                return Json(new { isValid = false, message = " اختر الحساب اولا" });

            }


            bool Stock = false;
            if (!string.IsNullOrEmpty(formCollection["Stock"])) { Stock = true; }
            PurchaseRe purchasedata = new PurchaseRe();
            PurchaseReDetail purchaseDetail1 = new PurchaseReDetail();
            var cookie = HttpContext.Request.Cookies.Get("AdminInfo");
            if (cookie != null)
            {
                var UserIn = JsonConvert.DeserializeObject<User>(Authentication.Decrypt(cookie.Value));
                if (UserIn != null)
                {
                    purchasedata.UserId = UserIn.Id;
                }

            }


            if (purchase.AccId == null)
            {
                purchasedata.AccId = 0;
            }
            else
            {
                purchasedata.AccId = purchase.AccId;

            }
            if (isCash == "1")
            {
                purchasedata.IsCash = true;
            }
            else if (isCash == "0")
            {
                purchasedata.IsCash = false;
            }
            purchasedata.ShiftId = SelectShiftID;
            purchasedata.Total = ProductsInCart.Sum(x => x.Total);
            purchasedata.Descount = purchase.Descount;
            purchasedata.Final = RPaied;
            purchasedata.Date = purchase.Date;
            if (purchase.StkId == null)
            {
                purchase.StkId = 0;

            }

            if (Stock == false)
            {
                purchasedata.StkId = purchase.StkId;
            }
            else
            {
                purchasedata.StkId = 0;

            }

            db.PurchaseRes.Add(purchasedata);
            db.SaveChanges();
            int Purchase_id = purchasedata.Id;
            foreach (var item in ProductsInCart)
            {
                var SelectPurprice = db.Items.Where(x => x.Id == item.ID).FirstOrDefault();
                purchaseDetail1.InvId = Purchase_id;
                purchaseDetail1.ItemId = item.ID;
                purchaseDetail1.Price = item.Price;
                purchaseDetail1.PurPrice = SelectPurprice.PricePur;

                purchaseDetail1.Count = item.Quantity;


                db.PurchaseReDetails.Add(purchaseDetail1);
                db.SaveChanges();


            }
            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("PurchaseReBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(2);
            HttpContext.Response.Cookies.Add(authCookie);

            return Json(new { isValid = true, message = "تم تسجيل الفاتورة بنجاح ", BillID = Purchase_id });
        }


        public ActionResult EditPurchaseRe(int id)
        {
            ViewBag.Stores = db.Stores.ToList();


            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("EditPurchaseReBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            var SelectpurchaseReDetails = db.PurchaseReDetails.Where(x => x.InvId == id).ToList();
            var SelectPurchaseRe = db.PurchaseRes.Where(x => x.Id == id).FirstOrDefault();
            var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseReBill");
            ViewBag.purchaseReDetails = SelectpurchaseReDetails;
            List<BillDetails> ProductsInCart = new List<BillDetails>();

            foreach (var item in SelectpurchaseReDetails)
            {
                BillDetails billDetails = new BillDetails()
                {
                    ID =(int) item.ItemId,
                    Name = item.Item.ItemName,
                    Price = float.Parse(item.PurPrice.ToString()),
                    Quantity = float.Parse(item.Count.ToString()),
                    Total = float.Parse((item.PurPrice * item.Count).ToString()),
                    FinalTotal = float.Parse(SelectPurchaseRe.Total.ToString()),
                    ItemsCount = SelectpurchaseReDetails.Count(),
                    TotalQuantity = float.Parse(SelectpurchaseReDetails.Sum(x => x.Count).ToString())

                };

                ProductsInCart.Add(billDetails);
                Serialize = JsonConvert.SerializeObject(ProductsInCart);
                cookieText = Authentication.Encrypt(Serialize);
                authCookie = new HttpCookie("EditPurchaseReBill", cookieText);
                authCookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(authCookie);
            }

            var date = SelectPurchaseRe.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ViewData["Dt"] = date;

            if (SelectPurchaseRe.IsCash == true)
            {
                ViewBag.PayOptionCash = "Checked";
            }
            else
            {
                ViewBag.PayoptionNoCash = "Checked";
            }
            if (SelectPurchaseRe.StkId == 0)
            {
                ViewBag.Stk = "Checked";
            }

            ViewBag.ItemCount = SelectpurchaseReDetails.Count();
            ViewBag.ItemsQuantity = SelectpurchaseReDetails.Sum(x => x.Count);
            ViewBag.AccId = SelectPurchaseRe.AccId;

            return View(SelectPurchaseRe);
        }



        public JsonResult CreateEditedBill(int ID = 0, string Name = null, float Price = 0, float Quantity = 0, float Total = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseReBill");

            if (ID > 0 && Name != null && Price > 0 && Quantity > 0 && Total > 0)
            {
                float TBill = 0;
                int ICount = 0;
                float IQuantity = 0;



                if (cookie2 != null)
                {
                    var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                    if (!ProductsInCart.Any(x => x.Name == Name))
                    {
                        foreach (var item in ProductsInCart)
                        {
                            TBill += item.Total;
                            ICount += 1;
                            IQuantity += item.Quantity;
                        }
                        BillDetails billDetails = new BillDetails()
                        {
                            ID = ID,
                            Name = Name,
                            Price = Price,
                            Quantity = Quantity,
                            Total = Total,
                            FinalTotal = TBill + Total,
                            ItemsCount = ICount + 1,
                            TotalQuantity = IQuantity + Quantity

                        };
                        ProductsInCart.Add(billDetails);
                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("EditPurchaseReBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    BillDetails billDetails = new BillDetails()
                    {
                        ID = ID,
                        Name = Name,

                        Price = Price,
                        Quantity = Quantity,
                        Total = Total,
                        FinalTotal = Total,
                        ItemsCount = 1,
                        TotalQuantity = Quantity


                    };

                    List<BillDetails> ProductsInCart = new List<BillDetails>();
                    ProductsInCart.Add(billDetails);
                    var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                    var cookieText = Authentication.Encrypt(Serialize);
                    var authCookie = new HttpCookie("EditPurchaseReBill", cookieText);
                    authCookie.Expires = DateTime.Now.AddDays(30);
                    HttpContext.Response.Cookies.Add(authCookie);
                    return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                }


            }
            if (cookie2 != null)
            {
                var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

            }
            return Json(new List<BillDetails>(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEditedItemTotalPrice(int ID = 0, float ItemQ = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseReBill");

            float TBill = 0;
            float IQuantity = 0;
            BillDetails billDetails = new BillDetails();
            if (ID > 0)
            {
                if (cookie2 != null)
                {
                    var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                    if (ProductsInCart.Any(x => x.ID == ID))
                    {


                        billDetails = ProductsInCart.Where(x => x.ID == ID).FirstOrDefault();


                        billDetails.Quantity = ItemQ;
                        billDetails.Total = ItemQ * billDetails.Price;
                        foreach (var item in ProductsInCart)
                        {
                            TBill += item.Total;
                            IQuantity += item.Quantity;
                        }
                        foreach (var item in ProductsInCart)
                        {
                            item.FinalTotal = TBill;
                            item.TotalQuantity = IQuantity;
                        }

                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("EditPurchaseReBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);


                    }



                    return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEditedItem(int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            float TBill = 0;

            float IQuantity = 0;
            if (ID > 0)
            {
                var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseReBill");
                if (cookie2 != null)
                {

                    var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));
                    if (ProductsInCart.Any(x => x.ID == ID))
                    {
                        int count = ProductsInCart.Count();
                        if (ProductsInCart.Count() == 1)
                        {

                            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
                        }
                        else
                        {
                            ProductsInCart.RemoveAll(y => y.ID == ID);
                            foreach (var item in ProductsInCart)
                            {
                                TBill += item.Total;
                                IQuantity += item.Quantity;

                            }
                            foreach (var item in ProductsInCart)
                            {
                                item.FinalTotal = TBill;
                                item.TotalQuantity = IQuantity;
                                item.ItemsCount = count - 1;
                            }

                        }

                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("EditPurchaseReBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult EditPurchaseRe(PurchaseRe purchase, string isCash, FormCollection formCollection)
        {
            int? SelectShiftID = db.GetShift_id().FirstOrDefault();
            if (SelectShiftID == null || SelectShiftID == 0)
            {
                return Json(new { isValid = false, message = "برجاء فتح فترة عمل اولا" });

            }
            List<BillDetails> ProductsInCart = new List<BillDetails>();
            var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseReBill");

            if (cookie2 != null)
            {
                ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                if (ProductsInCart == null)
                {
                    return Json(new { isValid = false, message = " لا توجد اصناف بالفاتورة " });

                }

            }
            else
            {
                return Json(new { isValid = false, message = " لا توجد اصناف بالفاتورة " });

            }


            if (isCash == "0" && (purchase.AccId == null || purchase.AccId <= 0))
            {
                return Json(new { isValid = false, message = " اختر الحساب اولا" });

            }

            var SelectpurchaseReDetails = db.PurchaseReDetails.Where(x => x.InvId == purchase.Id).ToList();
            var SelectPurchaseRe = db.PurchaseRes.Find(purchase.Id);
            foreach (var item in SelectpurchaseReDetails)
            {
                db.PurchaseReDetails.Remove(item);
                db.SaveChanges();
            }
            bool Stock = false;
            //bool Emp = false;
            if (!string.IsNullOrEmpty(formCollection["Stock"])) { Stock = true; }
            // if (!string.IsNullOrEmpty(formCollection["Emp"])) { Emp = true; }

            PurchaseReDetail Purchace_data = new PurchaseReDetail();
            var cookie = HttpContext.Request.Cookies.Get("AdminInfo");
            if (cookie != null)
            {
                var UserIn = JsonConvert.DeserializeObject<User>(Authentication.Decrypt(cookie.Value));
                if (UserIn != null)
                {
                    SelectPurchaseRe.UserId = UserIn.Id;
                }

            }
            SelectPurchaseRe.Date = purchase.Date;

            if (purchase.AccId == null)
            {
                SelectPurchaseRe.AccId = 0;
            }
            else
            {
                SelectPurchaseRe.AccId = purchase.AccId;

            }
            if (isCash == "1")
            {
                SelectPurchaseRe.IsCash = true;
            }
            else if (isCash == "0")
            {
                SelectPurchaseRe.IsCash = false;
            }
            SelectPurchaseRe.ShiftId = (int)SelectShiftID;
            SelectPurchaseRe.Total = ProductsInCart.Select(x => x.FinalTotal).FirstOrDefault();
            SelectPurchaseRe.Descount = purchase.Descount;
            SelectPurchaseRe.Final = purchase.Final;
            SelectPurchaseRe.Date = purchase.Date;
            //if (Emp == false)
            //{
            //    Purchacedata.emp_id = purchase.emp_id;
            //}
            if (Stock == false)
            {
                SelectPurchaseRe.StkId = purchase.StkId;
            }
            else
            {
                SelectPurchaseRe.StkId = 0;

            }
           
            SelectPurchaseRe.ShiftId = SelectShiftID;


            db.SaveChanges();
            int Purchase_id = purchase.Id;
            foreach (var item in ProductsInCart)
            {
                var SelectPurprice = db.Items.Where(x => x.Id == item.ID).FirstOrDefault();

                Purchace_data.InvId = Purchase_id;
                Purchace_data.ItemId = item.ID;
                Purchace_data.Price = item.Total;
                Purchace_data.PurPrice = SelectPurprice.PricePur;

                Purchace_data.Count = item.Quantity;

               

                db.PurchaseReDetails.Add(Purchace_data);
                db.SaveChanges();


            }
            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("EditPurchaseReBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(2);
            HttpContext.Response.Cookies.Add(authCookie);
            TempData["SuccessMessage"] = "تم تعديل الفاتورة بنجاح ";
            return Json(new { isValid = true, message = "تم تعديل الفاتورة بنجاح " });
        }



    }
}