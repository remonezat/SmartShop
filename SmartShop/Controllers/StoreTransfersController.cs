using Newtonsoft.Json;
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
    public class StoreTransfersController : BaseController
    {
        SmartShopEntities1 db = new SmartShopEntities1();

        // GET: StoreTransfers
        public ActionResult Add()
        {

            ViewBag.Stores = db.Stores.ToList();

            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("TranferBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            return View();
        }
        public JsonResult Getstkto(int stkid=0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Store> stock_List = db.Stores.Where(x => x.Id != stkid).ToList();
            return Json(stock_List, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllstkto(int stkid=0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Store> stock_List = db.Stores.ToList();
            return Json(stock_List, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateBill(int ID = 0, string Name = null, float Quantity = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("TranferBill");

            if (ID > 0 && Name != null  && Quantity > 0 )
            {
                float TBill = 0;
                int ICount = 0;
                float IQuantity = 0;
                var Q = db.ProcGetItemCredit_Stock(ID, stk_id).ToList();


                if (Q[0] >= Quantity)
                {
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
                                Quantity = Quantity,
                                ItemsCount = ICount + 1,
                                TotalQuantity = IQuantity + Quantity

                            };
                            ProductsInCart.Add(billDetails);
                            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                            var cookieText = Authentication.Encrypt(Serialize);
                            var authCookie = new HttpCookie("TranferBill", cookieText);
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
                            Quantity = Quantity,
                           
                            ItemsCount = 1,
                            TotalQuantity = Quantity


                        };

                        List<BillDetails> ProductsInCart = new List<BillDetails>();
                        ProductsInCart.Add(billDetails);
                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("TranferBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }

                }
                else
                {

                    if (cookie2 != null)
                    {
                        var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }

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
            var cookie2 = HttpContext.Request.Cookies.Get("TranferBill");

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
                        var Q = db.ProcGetItemCredit_Stock(ID, stk_id).ToList();
                        if (ItemQ <= float.Parse(Q[0].ToString()))
                        {

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
                            var authCookie = new HttpCookie("TranferBill", cookieText);
                            authCookie.Expires = DateTime.Now.AddDays(30);
                            HttpContext.Response.Cookies.Add(authCookie);
                        }
                        else
                        {
                            billDetails.Quantity = float.Parse(Q[0].ToString());
                            billDetails.Total = float.Parse(Q[0].ToString()) * billDetails.Price;
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
                            var authCookie = new HttpCookie("TranferBill", cookieText);
                            authCookie.Expires = DateTime.Now.AddDays(30);
                            HttpContext.Response.Cookies.Add(authCookie);
                        }

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
                var cookie2 = HttpContext.Request.Cookies.Get("TranferBill");
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
                        var authCookie = new HttpCookie("TranferBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Add(StoreTransfer stocks_Trans, FormCollection formCollection)
        {
            List<BillDetails> ProductsInCart = new List<BillDetails>();
            var cookie2 = HttpContext.Request.Cookies.Get("TranferBill");
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


            if ((stocks_Trans.StkFromId == null && stocks_Trans.StkToId == null)|| (stocks_Trans.StkFromId == 0 && stocks_Trans.StkToId == 0))
            {
                return Json(new { isValid = false, message = "اختر المخازن اولا " });

            }
            if (stocks_Trans.StkFromId==null)
            {
                stocks_Trans.StkFromId = 0;
            }
            foreach (var item in ProductsInCart)
            {
                if ( stocks_Trans.StkFromId != 0)
                {
                    var Q = db.ProcGetItemCredit_Stock(item.ID, stocks_Trans.StkFromId).ToList();
                    if (item.Quantity > float.Parse(Q[0].ToString()))
                    {

                        return Json(new { isValid = false, message = " لا تكفي " + item.Name + " كمية " });

                    }
                }
                else
                {
                    var Q = db.ProcGetItemCredit_Stock(item.ID, 0).ToList();
                    if (item.Quantity > float.Parse(Q[0].ToString()))
                    {

                        return Json(new { isValid = false, message = " لا تكفي " + item.Name + " كمية " });

                    }
                }

            }
            if (stocks_Trans.StkFromId == null)
            {
                stocks_Trans.StkFromId = 0;
            }
            if (stocks_Trans.StkToId == null)
            {
                stocks_Trans.StkToId = 0;

            }
            stocks_Trans.UserId = UserId;
            db.StoreTransfers.Add(stocks_Trans);
            db.SaveChanges();
            StoreTransferDetail stocks_Transdata = new StoreTransferDetail();
            foreach (var item in ProductsInCart)
            {
                stocks_Transdata.TransId = stocks_Trans.Id;
                stocks_Transdata.ItemId = item.ID;
                stocks_Transdata.Count = item.Quantity;

               
                db.StoreTransferDetails.Add(stocks_Transdata);
                db.SaveChanges();

            }

            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("TranferBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(2);
            HttpContext.Response.Cookies.Add(authCookie);

            return Json(new { isValid = true, message = "تم  تحويل بنجاح " });
        }

        public ActionResult StoresTransfers()
        {
            ViewBag.SuccessMessage = "";
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

        public JsonResult GetStoreTransaction(DateTime DateF, DateTime DateT, int IDFrom = 0, int IDTo = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Data> dt = new List<Data>();

           
                var select_Transaction = db.StoreTransfers.Where(x => x.Date >= DateF && x.Date <= DateT && x.StkFromId == IDFrom && x.StkToId == IDTo).ToList();
            foreach (var item in select_Transaction)
            {
                if (item.StkFromId == 0)
                {
                    Data Details = new Data()
                    {
                        id = item.Id,
                        dat = item.Date,
                        stkfrom = "المحل",
                        stkto = db.Stores.Where(x => x.Id == item.StkToId).Select(x => x.StoreName).FirstOrDefault().ToString(),

                    };
                    dt.Add(Details);

                }
                else if (item.StkToId == 0)
                {
                    Data Details = new Data()
                    {
                        id = item.Id,
                        dat = item.Date,
                        stkto = "المحل",
                        stkfrom = db.Stores.Where(x => x.Id == item.StkFromId).Select(x => x.StoreName).FirstOrDefault().ToString(),


                    };
                    dt.Add(Details);

                }
                else
                {


                    Data Details = new Data()
                    {
                        id = item.Id,
                        dat = item.Date,
                        stkfrom = db.Stores.Where(x => x.Id == item.StkFromId).Select(x => x.StoreName).FirstOrDefault().ToString(),
                        stkto = db.Stores.Where(x => x.Id == item.StkToId).Select(x => x.StoreName).FirstOrDefault().ToString(),


                    };
                    dt.Add(Details);


                }

            }


                return Json(dt, JsonRequestBehavior.AllowGet);

            
        

        }

        public ActionResult Delete(DateTime DateF, DateTime DateT, int IDFrom = 0, int IDTo = 0, int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (ID > 0)
            {
                var selecttransferdet = db.StoreTransferDetails.Where(x => x.TransId == ID).ToList();
                var selecttransfer = db.StoreTransfers.Where(x => x.Id == ID).FirstOrDefault();

                foreach (var item in selecttransferdet)
                {
                    var Quantity = db.ProcGetItemCredit_Stock(item.ItemId, selecttransfer.StkToId).FirstOrDefault();
                    if (Quantity < item.Count)
                    {
                        TempData["DeleteMessage"] = "لا يمكن حذف هذا التحويل";
                        return RedirectToAction("StoresTransfers");
                    }
                }

                foreach (var item in selecttransferdet)
                {
                    db.StoreTransferDetails.Remove(item);
                    db.SaveChanges();
                }
                db.StoreTransfers.Remove(selecttransfer);
                db.SaveChanges();

            }
            List<Data> dt = new List<Data>();


            var select_Transaction = db.StoreTransfers.Where(x => x.Date >= DateF && x.Date <= DateT && x.StkFromId == IDFrom && x.StkToId == IDTo).ToList();
            foreach (var item in select_Transaction)
            {
                if (item.StkFromId == 0)
                {
                    Data Details = new Data()
                    {
                        id = item.Id,
                        dat = item.Date,
                        stkfrom = "المحل",
                        stkto = db.Stores.Where(x => x.Id == item.StkToId).Select(x => x.StoreName).FirstOrDefault().ToString(),

                    };
                    dt.Add(Details);

                }
                else if (item.StkToId == 0)
                {
                    Data Details = new Data()
                    {
                        id = item.Id,
                        dat = item.Date,
                        stkto = "المحل",
                        stkfrom = db.Stores.Where(x => x.Id == item.StkFromId).Select(x => x.StoreName).FirstOrDefault().ToString(),


                    };
                    dt.Add(Details);

                }
                else
                {


                    Data Details = new Data()
                    {
                        id = item.Id,
                        dat = item.Date,
                        stkfrom = db.Stores.Where(x => x.Id == item.StkFromId).Select(x => x.StoreName).FirstOrDefault().ToString(),
                        stkto = db.Stores.Where(x => x.Id == item.StkToId).Select(x => x.StoreName).FirstOrDefault().ToString(),


                    };
                    dt.Add(Details);


                }

            }

            TempData["SuccessMessage"] = "تم الحذف ";

            return Json(dt, JsonRequestBehavior.AllowGet);



        }


        public ActionResult Edit(int ID)
        {
            var selecttransferdet = db.StoreTransferDetails.Where(x => x.TransId == ID).ToList();
            var selecttransfer = db.StoreTransfers.Find(ID);
            ViewBag.transferDetails = selecttransferdet;
            ViewBag.Stores = db.Stores.ToList();

            var Serialize = JsonConvert.SerializeObject(null);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("EditTranferBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Add(authCookie);
            var cookie2 = HttpContext.Request.Cookies.Get("EditTranferBill");
            List<BillDetails> ProductsInCart = new List<BillDetails>();


            if (selecttransfer.StkFromId == 0)
            {
                ViewBag.stk_from = "checked";
            }
            if (selecttransfer.StkToId == 0)
            {
                ViewBag.stk_to = "checked";

            }

            foreach (var item in selecttransferdet)
            {
                BillDetails billDetails = new BillDetails()
                {
                    ID =(int) item.ItemId,
                    Name = db.Items.Where(x => x.Id == item.ItemId).Select(x => x.ItemName).FirstOrDefault(),
                 
                    Quantity = (float)item.Count,
                    ItemsCount = selecttransferdet.Count(),
                    TotalQuantity = float.Parse(selecttransferdet.Sum(x => x.Count).ToString())

                };

                ProductsInCart.Add(billDetails);

            }
            Serialize = JsonConvert.SerializeObject(ProductsInCart);
            cookieText = Authentication.Encrypt(Serialize);
            authCookie = new HttpCookie("EditTranferBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Response.Cookies.Add(authCookie);
            ViewBag.transferDetails = ProductsInCart;

            var date = selecttransfer.Date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            ViewData["Dt"] = date;
            ViewBag.ItemCount = selecttransferdet.Count();
            ViewBag.ItemsQuantity = selecttransferdet.Sum(x => x.Count);
           


            return View(selecttransfer);
        }

        public JsonResult DeleteEditedItem(int ID = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;

            float TBill = 0;

            float IQuantity = 0;
            if (ID > 0)
            {
                var cookie2 = HttpContext.Request.Cookies.Get("EditTranferBill");
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
                                item.TotalQuantity = IQuantity;
                                item.ItemsCount = count - 1;
                            }

                        }

                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("EditTranferBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemEditTotalPrice(int ID = 0, float ItemQ = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("EditTranferBill");

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
                        var Q = db.ProcGetItemCredit_Stock(ID, stk_id).ToList();
                        if (ItemQ <= float.Parse(Q[0].ToString()))
                        {

                            billDetails.Quantity = ItemQ;
                            foreach (var item in ProductsInCart)
                            {
                                TBill += item.Total;
                                IQuantity += item.Quantity;
                            }
                            foreach (var item in ProductsInCart)
                            {
                                item.TotalQuantity = IQuantity;
                            }

                            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                            var cookieText = Authentication.Encrypt(Serialize);
                            var authCookie = new HttpCookie("EditTranferBill", cookieText);
                            authCookie.Expires = DateTime.Now.AddDays(30);
                            HttpContext.Response.Cookies.Add(authCookie);
                        }
                        else
                        {
                            billDetails.Quantity = float.Parse(Q[0].ToString());
                            foreach (var item in ProductsInCart)
                            {
                                TBill += item.Total;
                                IQuantity += item.Quantity;
                            }
                            foreach (var item in ProductsInCart)
                            {
                                item.TotalQuantity = IQuantity;
                            }

                            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                            var cookieText = Authentication.Encrypt(Serialize);
                            var authCookie = new HttpCookie("EditTranferBill", cookieText);
                            authCookie.Expires = DateTime.Now.AddDays(30);
                            HttpContext.Response.Cookies.Add(authCookie);
                        }

                    }



                    return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new SelectList(""), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateEditedBill(int ID = 0, string Name = null, float Quantity = 0, int stk_id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var cookie2 = HttpContext.Request.Cookies.Get("EditTranferBill");

            if (ID > 0 && Name != null  && Quantity > 0)
            {
                int ICount = 0;
                float IQuantity = 0;
                var Q = db.ProcGetItemCredit_Stock(ID, stk_id).ToList();


                if (Q[0] >= Quantity)
                {
                    if (cookie2 != null)
                    {
                        var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                        if (!ProductsInCart.Any(x => x.Name == Name))
                        {
                            foreach (var item in ProductsInCart)
                            {
                                ICount += 1;
                                IQuantity += item.Quantity;
                            }
                            BillDetails billDetails = new BillDetails()
                            {
                                ID = ID,
                                Name = Name,
                                Quantity = Quantity,
                                ItemsCount = ICount + 1,
                                TotalQuantity = IQuantity + Quantity

                            };
                            ProductsInCart.Add(billDetails);
                            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                            var cookieText = Authentication.Encrypt(Serialize);
                            var authCookie = new HttpCookie("EditTranferBill", cookieText);
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
                         
                            Quantity = Quantity,
                            ItemsCount = 1,
                            TotalQuantity = Quantity


                        };

                        List<BillDetails> ProductsInCart = new List<BillDetails>();
                        ProductsInCart.Add(billDetails);
                        var Serialize = JsonConvert.SerializeObject(ProductsInCart);
                        var cookieText = Authentication.Encrypt(Serialize);
                        var authCookie = new HttpCookie("EditTranferBill", cookieText);
                        authCookie.Expires = DateTime.Now.AddDays(30);
                        HttpContext.Response.Cookies.Add(authCookie);
                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }

                }
                else
                {

                    if (cookie2 != null)
                    {
                        var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                        return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

                    }

                }
            }
            if (cookie2 != null)
            {
                var ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                return Json(ProductsInCart, JsonRequestBehavior.AllowGet);

            }
            return Json(new List<BillDetails>(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(StoreTransfer stocks_Trans, FormCollection formCollection)
        {
            List<BillDetails> ProductsInCart = new List<BillDetails>();
            var cookie2 = HttpContext.Request.Cookies.Get("EditTranferBill");
            if (cookie2 != null)
            {
                ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));

                if (ProductsInCart.Count == 0)
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

            var SelectTransDetails = db.StoreTransferDetails.Where(x => x.TransId == stocks_Trans.Id).ToList();
            foreach (var item in SelectTransDetails)
            {
                db.StoreTransferDetails.Remove(item);
                db.SaveChanges();
            }

            ProductsInCart = JsonConvert.DeserializeObject<List<BillDetails>>(Authentication.Decrypt(cookie2.Value));



            if ((stocks_Trans.StkFromId == null && stocks_Trans.StkToId == null) || (stocks_Trans.StkFromId == 0 && stocks_Trans.StkToId == 0))
            {
                return Json(new { isValid = false, message = "اختر المخازن اولا " });

            }
            if (stocks_Trans.StkFromId == null)
            {
                stocks_Trans.StkFromId = 0;
            }

            foreach (var item in ProductsInCart)
            {
                if (stocks_Trans.StkFromId != 0)
                {
                    var Q = db.ProcGetItemCredit_Stock(item.ID, stocks_Trans.StkFromId).ToList();
                    if (item.Quantity > float.Parse(Q[0].ToString()))
                    {

                        return Json(new { isValid = false, message = " لا تكفي " + item.Name + " كمية " });

                    }
                }
                else
                {
                    var Q = db.ProcGetItemCredit_Stock(item.ID, 0).ToList();
                    if (item.Quantity > float.Parse(Q[0].ToString()))
                    {

                        return Json(new { isValid = false, message = " لا تكفي " + item.Name + " كمية " });

                    }
                }

            }
            if (stocks_Trans.StkFromId == null)
            {
                stocks_Trans.StkFromId = 0;
            }
            if (stocks_Trans.StkToId == null)
            {
                stocks_Trans.StkToId = 0;

            }
            stocks_Trans.UserId = UserId;
            var existingEntity = db.StoreTransfers.Find(stocks_Trans.Id);
            db.Entry(existingEntity).CurrentValues.SetValues(stocks_Trans);

            db.SaveChanges();
            StoreTransferDetail stocks_Transdata = new StoreTransferDetail();
            foreach (var item in ProductsInCart)
            {
                stocks_Transdata.TransId = stocks_Trans.Id;
                stocks_Transdata.ItemId = item.ID;
                stocks_Transdata.Count = item.Quantity;

               
                db.StoreTransferDetails.Add(stocks_Transdata);
                db.SaveChanges();

            }

            ProductsInCart.RemoveRange(0, ProductsInCart.Count());
            var Serialize = JsonConvert.SerializeObject(ProductsInCart);
            var cookieText = Authentication.Encrypt(Serialize);
            var authCookie = new HttpCookie("EditTranferBill", cookieText);
            authCookie.Expires = DateTime.Now.AddDays(2);
            HttpContext.Response.Cookies.Add(authCookie);

            TempData["SuccessMessage"] = "تم تعديل  بنجاح ";
            return Json(new { isValid = true, message = "تم تعديل الفاتورة بنجاح " });
        }


    }


    public class Data
    {
        public DateTime? dat { get; set; }
        public int id { get; set; }
        public string stkfrom { get; set; }
        public string stkto { get; set; }
     


    }
}