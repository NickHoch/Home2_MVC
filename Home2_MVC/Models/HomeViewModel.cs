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
            AvailableProducts = new SelectList(products, "Id", "Name", 1);
            Order = new Order();
        }
        public Order Order { get; set; }
        public SelectList AvailableProducts { get; set; }
    }

}