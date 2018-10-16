using Home2_MVC.ModelsDB;
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
            //AvailableProducts = new List<Product>();
        }
        public Order Order { get; set; }
        //public List<Product> AvailableProducts { get; set; }
        public SelectList AvailableProducts { get; set; }
    }

}