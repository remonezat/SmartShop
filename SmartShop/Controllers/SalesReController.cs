using SmartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartShop.Utilites;
using Newtonsoft.Json;
using SmartShop.PublicClasses;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace SmartShop.Controllers
{
    public class SalesReController : BaseController
    {
        SmartShopEntities1 db = new SmartShopEntities1();

        // GET: SalesRe
        public ActionResult Add()
        {
            ViewBag.Stores = db.Stores.ToList();

            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("SalesReBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            return View();
        }



        public JsonResult CreateBill(int ID = 0, string Name = null, float Price = 0, float Quantity = 0, float Total = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("SalesReBill");

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
                            var authCookie = new HttpCookie("SalesReBill", cookieText);
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
                        var authCookie = new HttpCookie("SalesReBill", cookieText);
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
            var cookie2 = HttpContext.Request.Cookies.Get("SalesReBill");

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
                            var authCookie = new HttpCookie("SalesReBill", cookieText);
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
                var cookie2 = HttpContext.Request.Cookies.Get("SalesReBill");
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
                        var authCookie = new HttpCookie("SalesReBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Add(SalesRe sale, SalesReDetail sales_Det, string isCash, FormCollection formCollection, float RPaied)
        {


            int? SelectShiftID = db.GetShift_id().FirstOrDefault();
            if (SelectShiftID == null || SelectShiftID == 0)
            {
                return Json(new { isValid = false, message = "برجاء فتح فترة عمل اولا" });

            }

            List<BillDetails> ProductsInCart = new List<BillDetails>();
            var cookie2 = HttpContext.Request.Cookies.Get("SalesReBill");
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


            if (isCash == "0" && (sale.AccId == null || sale.AccId <= 0))
            {
                return Json(new { isValid = false, message = " اختر الحساب اولا" });

            }
            foreach (var item in ProductsInCart)
            {
                if (sale.StkId == null)
                {
                    sale.StkId = 0;

                }
                var Q = db.ProcGetItemCredit_Stock(item.ID, sale.StkId).ToList();
                if (item.Quantity > float.Parse(Q[0].ToString()))
                {

                    return Json(new { isValid = false, message = " لا تكفي " + item.Name + " كمية " });

                }

            }

            bool Stock = false;
            if (!string.IsNullOrEmpty(formCollection["Stock"])) { Stock = true; }
            SalesRe saledata = new SalesRe();
            SalesReDetail sales_data = new SalesReDetail();
            var cookie = HttpContext.Request.Cookies.Get("AdminInfo");
            if (cookie != null)
            {
                var UserIn = JsonConvert.DeserializeObject<User>(Authentication.Decrypt(cookie.Value));
                if (UserIn != null)
                {
                    saledata.UserId = UserIn.Id;
                }

            }


            if (sale.AccId == null)
            {
                saledata.AccId = 0;
            }
            else
            {
                saledata.AccId = sale.AccId;

            }
            if (isCash == "1")
            {
                saledata.IsCash = true;
            }
            else if (isCash == "0")
            {
                saledata.IsCash = false;
            }
            saledata.ShiftId = SelectShiftID;
            saledata.Total = ProductsInCart.Sum(x => x.Total);
            saledata.Descount = sale.Descount;
            saledata.Final = RPaied;
            saledata.Date = sale.Date;


            if (Stock == false)
            {
                saledata.StkId = sale.StkId;
            }
            else
            {
                saledata.StkId = 0;

            }

            db.SalesRes.Add(saledata);
            db.SaveChanges();
            int Sales_id = saledata.Id;
            foreach (var item in ProductsInCart)
            {
                var SelectPurprice = db.Items.Where(x => x.Id == item.ID).FirstOrDefault();

                sales_data.InvId = Sales_id;
                sales_data.ItemId = item.ID;
                sales_data.Price = item.Price;
                sales_data.PurPrice = SelectPurprice.PricePur;

                sales_data.Count = item.Quantity;


                db.SalesReDetails.Add(sales_data);
                db.SaveChanges();


            }
            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("SalesReBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(2);
            HttpContext.Response.Cookies.Add(authCookie);

            return Json(new { isValid = true, message = "تم تسجيل الفاتورة بنجاح ", BillID = Sales_id });
        }
        public ActionResult EditSalesRe(int id)
        {
            ViewBag.Stores = db.Stores.ToList();



            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("EditSalesReBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);

            var SelectSalesReDetails = db.SalesReDetails.Where(x => x.InvId == id).ToList();
            var SelectSalesRe = db.SalesRes.Where(x => x.Id == id).FirstOrDefault();
            var cookie2 = HttpContext.Request.Cookies.Get("EditSalesReBill");
            ViewBag.SalesReDetails = SelectSalesReDetails;
            List<BillDetails> ProductsInCart = new List<BillDetails>();

            foreach (var item in SelectSalesReDetails)
            {
                BillDetails billDetails = new BillDetails()
                {
                    ID =(int) item.ItemId,

                    Name = item.Item.ItemName,
                 
                    Price = float.Parse(item.Price.ToString()),
                    Quantity = float.Parse(item.Count.ToString()),
                    Total = float.Parse((item.Price * item.Count).ToString()),
                    FinalTotal = float.Parse(SelectSalesRe.Total.ToString()),
                    ItemsCount = SelectSalesReDetails.Count(),
                    TotalQuantity = float.Parse(SelectSalesReDetails.Sum(x => x.Count).ToString())

                };

                ProductsInCart.Add(billDetails);
                Serialize = JsonConvert.SerializeObject(ProductsInCart);
                cookieText = Authentication.Encrypt(Serialize);
                authCookie = new HttpCookie("EditSalesReBill", cookieText);
                authCookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Add(authCookie);
            }
            var date = SelectSalesRe.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ViewData["Dt"] = date;
           
            if (SelectSalesRe.IsCash == true)
            {
                ViewBag.PayOptionCash = "Checked";
            }
            else
            {
                ViewBag.PayoptionNoCash = "Checked";
            }
            if (SelectSalesRe.StkId == 0)
            {
                ViewBag.stk = "Checked";
            }


            ViewBag.ItemCount = SelectSalesReDetails.Count();
            ViewBag.ItemsQuantity = SelectSalesReDetails.Sum(x => x.Count);
            ViewBag.AccId = SelectSalesRe.AccId;
            return View(SelectSalesRe);
        }



        public JsonResult CreateEditedBill(int ID = 0, string Name = null, float Price = 0, float Quantity = 0, float Total = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("EditSalesReBill");

            if (ID > 0 && Name != null && Price > 0 && Quantity > 0 && Total > 0)
            {
                float TBill = 0;
                int ICount = 0;
                float IQuantity = 0;
                var Q = db.ProcGetItemCredit_Stock(ID, stk_id).ToList();


               
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
                            var authCookie = new HttpCookie("EditSalesReBill", cookieText);
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
                        var authCookie = new HttpCookie("EditSalesReBill", cookieText);
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
            var cookie2 = HttpContext.Request.Cookies.Get("EditSalesReBill");

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
                            var authCookie = new HttpCookie("EditSalesReBill", cookieText);
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
                var cookie2 = HttpContext.Request.Cookies.Get("EditSalesReBill");
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
                        var authCookie = new HttpCookie("EditSalesReBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult EditSalesRe(SalesRe sale_re, string isCash, FormCollection formCollection)
        {
            int? SelectShiftID = db.GetShift_id().FirstOrDefault();
            if (SelectShiftID == null || SelectShiftID == 0)
            {
                return Json(new { isValid = false, message = "برجاء فتح فترة عمل اولا" });

            }
            List<BillDetails> ProductsInCart = new List<BillDetails>();
            var cookie2 = HttpContext.Request.Cookies.Get("EditSalesReBill");

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



            if (isCash == "0" && (sale_re.AccId == null || sale_re.AccId <= 0))
            {
                return Json(new { isValid = false, message = " اختر الحساب اولا" });

            }
            var SelectSalesReDetails = db.SalesReDetails.Where(x => x.InvId == sale_re.Id).ToList();
            var SelectSalesRe = db.SalesRes.Find(sale_re.Id);
            foreach (var item in SelectSalesReDetails)
            {
                db.SalesReDetails.Remove(item);
                db.SaveChanges();
            }
           

            bool Stock = false;
            if (!string.IsNullOrEmpty(formCollection["Stock"])) { Stock = true; }
            SalesReDetail sales_data = new SalesReDetail();
            var cookie = HttpContext.Request.Cookies.Get("AdminInfo");
            if (cookie != null)
            {
                var UserIn = JsonConvert.DeserializeObject<User>(Authentication.Decrypt(cookie.Value));
                if (UserIn != null)
                {
                    SelectSalesRe.UserId = UserIn.Id;
                }

            }
            SelectSalesRe.Date = sale_re.Date;

            if (sale_re.AccId == null)
            {
                SelectSalesRe.AccId = 0;
            }
            else
            {
                SelectSalesRe.AccId = sale_re.AccId;

            }
            if (isCash == "1")
            {
                SelectSalesRe.IsCash = true;
            }
            else if (isCash == "0")
            {
                SelectSalesRe.IsCash = false;
            }
            SelectSalesRe.ShiftId = (int)SelectShiftID;
            SelectSalesRe.Total = ProductsInCart.Select(x => x.FinalTotal).FirstOrDefault();
            SelectSalesRe.Descount = sale_re.Descount;
            SelectSalesRe.Final = sale_re.Final;
            SelectSalesRe.Date = sale_re.Date;
           
            if (Stock == false)
            {
                SelectSalesRe.StkId = sale_re.StkId;
            }
            else
            {
                SelectSalesRe.StkId = 0;

            }
         
            SelectSalesRe.ShiftId = SelectShiftID;
            db.SaveChanges();
            int Sales_id = sale_re.Id;
            foreach (var item in ProductsInCart)
            {
                var SelectPurprice = db.Items.Where(x => x.Id == item.ID).FirstOrDefault();

                sales_data.InvId = Sales_id;
                sales_data.ItemId = item.ID;
                sales_data.Price = item.Price;
                sales_data.Count = item.Quantity;
                sales_data.PurPrice = SelectPurprice.PricePur;


                db.SalesReDetails.Add(sales_data);
                db.SaveChanges();

            }
            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("EditSalesReBill", cookieText);
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

                var SelectBill = db.SalesRes.Where(x => x.Id == ID).FirstOrDefault();
                var SelectBillDetails = db.SalesReDetails.Where(x => x.InvId == ID).ToList();

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