﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Home2_MVC.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}