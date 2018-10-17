using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Home2_MVC.Models;

namespace Home2_MVC.Controllers
{
    public class HomeController : Controller
    {
        public Model _ctx = new Model();

        [HttpGet]
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel(_ctx.Products.ToList());
            if (Session["Bucket"] != null)
            {
                var bucket = Session["Bucket"];
                var bucketDictionary = bucket as List<ItemOrder>;
                model.Order.Items = bucketDictionary;
                Session["Bucket"] = null;
                Session["Bucket"] = bucket;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            HomeViewModel model = new HomeViewModel(_ctx.Products.ToList());
            if(formCollection["name"] != null)
            {
                var order = new Order
                {
                    Info = new ContactInfo
                    {
                        Name = formCollection["name"],
                        PhoneNumber = formCollection["phonenumber"]
                    }
                };
                _ctx.Orders.Add(order);
                var items = Session["Bucket"] as List<ItemOrder>;
                items.ForEach(x => x.Order = order);
                _ctx.ItemOrders.AddRange(items);
                _ctx.SaveChanges();
                //_ctx.Orders.Add(new Order
                //{
                //    Info = new ContactInfo
                //    {
                //        Name = formCollection["name"],
                //        PhoneNumber = formCollection["phonenumber"]
                //    },
                //    Items = Session["Bucket"] as List<ItemOrder>
                //});
                //_ctx.SaveChanges();
                return View(model);
            }

            int idProd = Convert.ToInt32(formCollection["products"]);
            string Name = _ctx.Products.SingleOrDefault(x => x.Id == idProd).Name;
            Product product = new Product(idProd, Name);
            int quanProd = Convert.ToInt32(formCollection["quantity"]);

            if (Session["Bucket"] == null)
            {
                model.Order.Items.Add(new ItemOrder
                {
                    Product = product,
                    Quantity = quanProd
                });
                Session["Bucket"] = new List<ItemOrder>()
                {
                    new ItemOrder
                    {
                        Product = product,
                        Quantity = quanProd
                    }
                };
            }
            else
            {                
                var bucket = Session["Bucket"];
                var bucketList = bucket as List<ItemOrder>;
                bucketList.Add(new ItemOrder
                {
                    Product = product,
                    Quantity = quanProd
                });
                model.Order.Items = bucketList;
                Session["Bucket"] = null;
                Session["Bucket"] = bucketList;
            }            
            return View(model);
        }

        public ActionResult MakeOrder()
        {
            Session["Bucket"] = null;
            return PartialView("_PopUp", new ContactInfo());
        }

        //[HttpPost]
        //public ActionResult Index(FormCollection formCollection, string submitButton)
        //{
        //    switch (submitButton)
        //    {
        //        case "AddToBucket":
        //            return AddToBucket(formCollection);
        //        case "MakeOrder":
        //            return MakeOrder();
        //        default:
        //            return View();
        //    }
        //}

        private ActionResult AddToBucket(FormCollection formCollection)
        {
            int idProd = Convert.ToInt32(formCollection["products"]);
            string Name = _ctx.Products.SingleOrDefault(x => x.Id == idProd).Name;
            int quanProd = Convert.ToInt32(formCollection["quantity"]);
            Product product = new Product(idProd, Name);
            if (Session["Bucket"] == null)
            {
                Session["Bucket"] = new List<ItemOrder>()
                {
                    new ItemOrder
                    {
                        Product = product,
                        Quantity = quanProd
                    }
                };
            }
            else
            {
                var bucket = Session["Bucket"];
                (bucket as List<ItemOrder>).Add(new ItemOrder
                {
                    Product = product,
                    Quantity = quanProd
                });
                Session["Bucket"] = bucket;
            }
            HomeViewModel model = new HomeViewModel(_ctx.Products.ToList());
            return View(model);
        }

        public ActionResult SignIn()
        {
            NameValueCollection nvc = Request.Form;
            string login = String.Empty;
            string password = String.Empty;
            if (!string.IsNullOrEmpty(nvc["login"]))
            {
                login = nvc["login"];
            }
            if (!string.IsNullOrEmpty(nvc["password"]))
            {
                password = nvc["password"];
            }
            Model _ctx = new Model();

            var user = _ctx.Users.SingleOrDefault(x => x.Login == login);
            if (user != null)
            {
                var passwordDb = user.Password;
                var passwordHashed = Models.Hash.HashPassword(password);

                if (String.Equals(passwordHashed, passwordDb))
                {
                    var token = Guid.NewGuid().ToString();
                    user.Token = token;
                    _ctx.SaveChanges();
                    if (user.IsAdmin)
                    {
                        Response.Cookies.Add(new HttpCookie("IsAdmin", "1"));
                    }
                    else
                    {
                        Response.Cookies.Add(new HttpCookie("IsAdmin", "0"));
                    }
                    Response.Cookies.Add(new HttpCookie("Token", token));
                    Response.Cookies.Add(new HttpCookie("User", user.Login));
                    //ViewBag.User = user;
                }
            }
            return View("Index");
        }

        public ActionResult LogOut()
        {
            if (Request.Cookies["IsAdmin"] != null)
            {
                var c = new HttpCookie("IsAdmin");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            if (Request.Cookies["Token"] != null)
            {
                var c = new HttpCookie("Token");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return View("Index");
        }
    }
}


//int idProd = Convert.ToInt32(formCollection["products"]);
//string Name = _ctx.Products.SingleOrDefault(x => x.Id == idProd).Name;
//int quanProd = Convert.ToInt32(formCollection["quantity"]);

//Session["Order"] = new Order
//{
//    User = new Models.User
//    {
//        Name = "Vasya",
//        PhoneNumber = "+380988877755"
//    },
//    Items = new Dictionary<Product, int>()
//    {
//        {
//            new Product(idProd, Name),
//            quanProd
//        }
//    }
//};
