using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home2_MVC.Models
{
    public class ItemOrder
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}