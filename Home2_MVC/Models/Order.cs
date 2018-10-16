using Home2_MVC.ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home2_MVC.Models
{
    public class Order
    {
        public Order()
        {
            Items = new Dictionary<Product, int>();
        }
        public ContactInfo Info { get; set; }
        public Dictionary<Product, int> Items { get; set; }
    }
}