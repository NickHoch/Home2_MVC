using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Home2_MVC.Models;
using Home2_MVC.ModelsDB;

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
                var bucketDictionary = bucket as Dictionary<Product, int>;
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

            int idProd = Convert.ToInt32(formCollection["products"]);
            string Name = _ctx.Products.SingleOrDefault(x => x.Id == idProd).Name;
            Product product = new Product(idProd, Name);
            int quanProd = Convert.ToInt32(formCollection["quantity"]);

            if (Session["Bucket"] == null)
            {
                model.Order.Items.Add(product, quanProd);
                Session["Bucket"] = new Dictionary<Product, int>()
                {
                    {
                        product,
                        quanProd
                    }
                };
            }
            else
            {
                
                var bucket = Session["Bucket"];
                var bucketDictionary = bucket as Dictionary<Product, int>;
                bucketDictionary.Add(product, quanProd);
                model.Order.Items = bucketDictionary;
                Session["Bucket"] = null;
                Session["Bucket"] = bucket;
            }            
            return View(model);
        }

        public ActionResult MakeOrder()
        {
            //if (Session["Bucket"] != null)
            //{
            //    var order = new Order
            //    {
            //        User = new Models.User
            //        {
            //            Name = "Vasya",
            //            PhoneNumber = "+380988877755"
            //        },
            //        Items = Session["Bucket"] as Dictionary<Product, int>
            //    };
            //}
            ContactInfo info = new ContactInfo();
            Session["Bucket"] = null;
            return PartialView("PopUp", info);

            //HomeViewModel model = new HomeViewModel(_ctx.Products.ToList());
            //return View(model);
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
                Session["Bucket"] = new Dictionary<Product, int>()
                {
                    {
                        product,
                        quanProd
                    }
                };
            }
            else
            {
                var bucket = Session["Bucket"];
                (bucket as Dictionary<Product, int>).Add(product, quanProd);
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
                var passwordHashed = ModelsDB.Hash.HashPassword(password);

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
