using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Home2_MVC.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(List<Product> products)
        {
            Products = products;
            Order = new Order();
        }
        public Order Order { get; set; }
        public List<Product> Products { get; set; }
    }

}