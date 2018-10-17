using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home2_MVC.Models
{
    public class Product
    {
        public Product() { }
        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}