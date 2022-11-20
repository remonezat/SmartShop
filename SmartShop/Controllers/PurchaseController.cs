using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using SmartShop.Models;
using SmartShop.PublicClasses;
using SmartShop.Utilites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SmartShop.Controllers
{
    public class PurchaseController : BaseController
    {
        SmartShopEntities1 db = new SmartShopEntities1();

        // GET: Purchase
        public ActionResult Add()
        {
            ViewBag.Stores = db.Stores.ToList();

            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("PurchaseBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            return View();
        }
        public JsonResult GetCustomerData(string Name = null)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (Name != null)
            {
                List<Account> SelectCustomers = db.Accounts.Where(x => x.AccName.Contains(Name) && (x.AccType == "s" || x.AccType == "b")).ToList();


                return Json(SelectCustomers, JsonRequestBehavior.AllowGet);
            }

            return Json(new List<Account>(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomerDetails(int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID != 0)
            {
                List<Account> SelectCustomerdata = db.Accounts.Where(x => x.Id == ID).ToList();


                return Json(SelectCustomerdata, JsonRequestBehavior.AllowGet);
            }

            return Json(new List<Account>(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccBalance(int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID != 0)
            {
                var Q = db.Get_PersonAccountCredit(ID);


                return Json(Q, JsonRequestBehavior.AllowGet);
            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemData(string Name = null)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (Name != null)
            {
                List<Item> SelectItems = db.Items.Where(x => x.ItemName.Contains(Name)).ToList();


                return Json(SelectItems, JsonRequestBehavior.AllowGet);
            }

            return Json(new List<Item>(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemDetails(int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID != 0)
            {
                List<Item> SelectItemdata = db.Items.Where(x => x.Id == ID).ToList();


                return Json(SelectItemdata, JsonRequestBehavior.AllowGet);
            }

            return Json(new List<Item>(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItemQuantity(int ID = 0, int StkID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ID != 0)
            {
                var Q = db.ProcGetItemCredit_Stock(ID, StkID);


                return Json(Q, JsonRequestBehavior.AllowGet);
            }

            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateBill(int ID = 0, string Name = null, float Price = 0, float Quantity = 0, float Total = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("PurchaseBill");

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
                        var authCookie = new HttpCookie("PurchaseBill", cookieText);
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
                    var authCookie = new HttpCookie("PurchaseBill", cookieText);
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
            var cookie2 = HttpContext.Request.Cookies.Get("PurchaseBill");

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
                            var authCookie = new HttpCookie("PurchaseBill", cookieText);
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
                var cookie2 = HttpContext.Request.Cookies.Get("PurchaseBill");
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
                        var authCookie = new HttpCookie("PurchaseBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Add(Purchase purchase, PurchaseDetail purchaseDetail, string isCash, FormCollection formCollection, float RPaied)
        {


            int? SelectShiftID = db.GetShift_id().FirstOrDefault();
            if (SelectShiftID == null || SelectShiftID == 0)
            {
                return Json(new { isValid = false, message = "برجاء فتح فترة عمل اولا" });

            }

            List<BillDetails> ProductsInCart = new List<BillDetails>();
            var cookie2 = HttpContext.Request.Cookies.Get("PurchaseBill");
            if (cookie2 != null)
            {
                ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                if (ProductsInCart == null)
                {
                    return Json(new { isValid = false, message = " لا توجد اصناف بالفاتورة " });

                }
                else if (ProductsInCart.Count == 0)
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
            Purchase purchasedata = new Purchase();
            PurchaseDetail purchaseDetail1 = new PurchaseDetail();
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

            db.Purchases.Add(purchasedata);
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


                db.PurchaseDetails.Add(purchaseDetail1);
                db.SaveChanges();


            }
            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("PurchaseBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(2);
            HttpContext.Response.Cookies.Add(authCookie);

            return Json(new { isValid = true, message = "تم تسجيل الفاتورة بنجاح ", BillID = Purchase_id });
        }



        public ActionResult EditPurchase(int id)
        {

            ViewBag.Stores = db.Stores.ToList();

            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("EditPurchaseBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            var SelectpurchaseDetails = db.PurchaseDetails.Where(x => x.InvId == id).ToList();
            var SelectPurchase = db.Purchases.Where(x => x.Id == id).FirstOrDefault();
            var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseBill");
            ViewBag.purchaseDetails = SelectpurchaseDetails;
            List<BillDetails> ProductsInCart = new List<BillDetails>();

            foreach (var item in SelectpurchaseDetails)
            {
                BillDetails billDetails = new BillDetails()
                {
                    ID =(int) item.ItemId,
                    Name = item.Item.ItemName,
                   
                    Price = float.Parse(item.PurPrice.ToString()),
                    Quantity = float.Parse(item.Count.ToString()),
                    Total = float.Parse(item.PurPrice.ToString()),
                    FinalTotal = float.Parse(SelectPurchase.Total.ToString()),
                    ItemsCount = SelectpurchaseDetails.Count(),
                    TotalQuantity = float.Parse(SelectpurchaseDetails.Sum(x => x.Count).ToString())

                };

                ProductsInCart.Add(billDetails);
                Serialize = JsonConvert.SerializeObject(ProductsInCart);
                cookieText = Authentication.Encrypt(Serialize);
                authCookie = new HttpCookie("EditPurchaseBill", cookieText);
                authCookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(authCookie);
            }

            var date = SelectPurchase.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ViewData["Dt"] = date;

            if (SelectPurchase.IsCash == true)
            {
                ViewBag.PayOptionCash = "Checked";
            }
            else
            {
                ViewBag.PayoptionNoCash = "Checked";
            }
            if (SelectPurchase.StkId == 0)
            {
                ViewBag.stk = "Checked";
            }


            ViewBag.ItemCount = SelectpurchaseDetails.Count();
            ViewBag.ItemsQuantity = SelectpurchaseDetails.Sum(x => x.Count);
            ViewBag.AccId = SelectPurchase.AccId;
            return View(SelectPurchase);
        }



        public JsonResult CreateEditedBill(int ID = 0, string Name = null, float Price = 0, float Quantity = 0, float Total = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseBill");

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
                        var authCookie = new HttpCookie("EditPurchaseBill", cookieText);
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
                    var authCookie = new HttpCookie("EditPurchaseBill", cookieText);
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
            var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseBill");

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
                        var authCookie = new HttpCookie("EditPurchaseBill", cookieText);
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
                var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseBill");
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
                        var authCookie = new HttpCookie("EditPurchaseBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditPurchase(Purchase purchase, string isCash, FormCollection formCollection)
        {
            int? SelectShiftID = db.GetShift_id().FirstOrDefault();
            if (SelectShiftID == null || SelectShiftID == 0)
            {
                return Json(new { isValid = false, message = "برجاء فتح فترة عمل اولا" });

            }
            List<BillDetails> ProductsInCart = new List<BillDetails>();
            var cookie2 = HttpContext.Request.Cookies.Get("EditPurchaseBill");
            if (cookie2 != null)
            {
                ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                if (ProductsInCart == null)
                {
                    return Json(new { isValid = false, message = " لا توجد اصناف بالفاتورة " });

                }
                else if (ProductsInCart.Count == 0)
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
            var SelectpurchaseDetails = db.PurchaseDetails.Where(x => x.InvId == purchase.Id).ToList();
            var SelectPurchase = db.Purchases.Find(purchase.Id);
            foreach (var item in SelectpurchaseDetails)
            {
                db.PurchaseDetails.Remove(item);
                db.SaveChanges();
            }


            bool Stock = false;
            //bool Emp = false;
            if (!string.IsNullOrEmpty(formCollection["Stock"])) { Stock = true; }
            // if (!string.IsNullOrEmpty(formCollection["Emp"])) { Emp = true; }
            PurchaseDetail Purchace_data = new PurchaseDetail();
            var cookie = HttpContext.Request.Cookies.Get("AdminInfo");
            if (cookie != null)
            {
                var UserIn = JsonConvert.DeserializeObject<User>(Authentication.Decrypt(cookie.Value));
                if (UserIn != null)
                {
                    SelectPurchase.UserId = UserIn.Id;
                }

            }
            SelectPurchase.Date = purchase.Date;

            if (purchase.AccId == null)
            {
                SelectPurchase.AccId = 0;
            }
            else
            {
                SelectPurchase.AccId = purchase.AccId;

            }
            if (isCash == "1")
            {
                SelectPurchase.IsCash = true;
            }
            else if (isCash == "0")
            {
                SelectPurchase.IsCash = false;
            }
            SelectPurchase.ShiftId = (int)SelectShiftID;
            SelectPurchase.Total = ProductsInCart.Sum(x => x.Total);
            SelectPurchase.Descount = purchase.Descount;
            SelectPurchase.Final = purchase.Final;
            SelectPurchase.Date = purchase.Date;
            //if (Emp == false)
            //{
            //    Purchacedata.emp_id = purchase.emp_id;
            //}
            if (Stock == false)
            {
                SelectPurchase.StkId = purchase.StkId;
            }
            else
            {
                SelectPurchase.StkId = 0;

            }
           

            db.SaveChanges();
            int Purchase_id = purchase.Id;
            foreach (var item in ProductsInCart)
            {
                var SelectPurprice = db.Items.Where(x => x.Id == item.ID).FirstOrDefault();

                Purchace_data.InvId = Purchase_id;
                Purchace_data.ItemId = item.ID;
                Purchace_data.Price = item.Price;
                Purchace_data.PurPrice = SelectPurprice.PricePur;
                Purchace_data.Count = item.Quantity;
                



                db.PurchaseDetails.Add(Purchace_data);
                db.SaveChanges();


            }
            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("EditPurchaseBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(2);
            HttpContext.Response.Cookies.Add(authCookie);
            TempData["SuccessMessage"] = "تم تعديل الفاتورة بنجاح ";
            return Json(new { isValid = true, message = "تم تعديل الفاتورة بنجاح " });
        }






        public ActionResult ExportToPDFRoll(int ID)
        {

            if (ID > 0)
            {
                string RptPath;
                ReportDataSource rds = new ReportDataSource();
                ReportDataSource rds2 = new ReportDataSource();
                ReportViewer rv = new ReportViewer();

                var SelectBill = db.Purchases.Where(x => x.Id == ID).FirstOrDefault();
                var SelectBillDetails = db.PurchaseDetails.Where(x => x.InvId == ID).ToList();

                var selectname = db.Accounts.Where(x => x.Id == SelectBill.AccId).Select(x => x.AccName).FirstOrDefault();






                List<BillData> Salesdata = new List<BillData>();
                foreach (var item in SelectBillDetails)
                {
                    if (selectname == null)
                    {
                        Salesdata.Add(new BillData()
                        {
                            iname = item.Item.ItemName,
                            count = item.Count.ToString(),
                            price = item.Price.ToString(),
                            total = (item.Price * item.Count).ToString(),
                            accname = "",
                            descount = SelectBill.Descount.ToString(),
                            final = SelectBill.Final.ToString(),


                        });
                    }
                    else
                    {
                        Salesdata.Add(new BillData()
                        {
                            iname = item.Item.ItemName,
                            count = item.Count.ToString(),
                            price = item.Price.ToString(),
                            total = (item.Price * item.Count).ToString(),
                            accname = selectname,
                            descount = SelectBill.Descount.ToString(),
                            final = SelectBill.Final.ToString(),

                        });
                    }
                }
                DataTable BillDatadt = new DataTable();

                BillDatadt.Columns.Add("iname", typeof(string));
                BillDatadt.Columns.Add("count", typeof(string));
                BillDatadt.Columns.Add("price", typeof(string));

                BillDatadt.Columns.Add("total", typeof(string));
                BillDatadt.Columns.Add("accname", typeof(string));
                BillDatadt.Columns.Add("descount", typeof(string));
                BillDatadt.Columns.Add("final", typeof(string));



                foreach (var item in Salesdata)
                {
                    BillDatadt.Rows.Add(item.iname, item.count, item.price, item.total, item.accname, item.descount, item.final);
                }


                rds.Name = "DataSet1";
                rds.Value = BillDatadt;

                RptPath = Server.MapPath(@"~\Reports\Bills\SalesBillA4.rdlc");

                rv.ProcessingMode = ProcessingMode.Local;
                rv.LocalReport.ReportPath = RptPath;





                Microsoft.Reporting.WebForms.LocalReport rpt = new Microsoft.Reporting.WebForms.LocalReport();

                /* Bind Here Report Data Set */
                string filename = "فاتورة مبيعات " + ID + DateTime.Now.ToString() + ".pdf";
                byte[] streamBytes = null;
                string mimeType = "";
                string encoding = "";
                string filenameExtension = "";
                string[] streamids = null;
                Warning[] warnings = null;


                // Add the new report datasource to the report.
                rv.LocalReport.DataSources.Add(rds);


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                rv.LocalReport.EnableHyperlinks = true;


                rv.LocalReport.Refresh();

                streamBytes = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                // This will download the pdf file
                //return File(streamBytes, mimeType, filename);

                //This will open directly the pdf file
                string Agent = HttpContext.Request.Headers["User-Agent"].ToString();

                //create and set PdfStamper  

                if (Agent.Contains("Android"))
                    return File(streamBytes, "application/pdf", filename);
                else
                    return File(streamBytes, "application/pdf");

            }
            return RedirectToAction("");


        }






    }
}