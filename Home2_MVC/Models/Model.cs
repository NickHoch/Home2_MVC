using Home2_MVC.Models;
using System.Data.Entity;

namespace Home2_MVC.Models
{
    public class Model : DbContext
    {
        public Model() : base("name=Model")
        {
            Database.SetInitializer<Model>(new CustomInitializer<Model>());
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ItemOrder> ItemOrders { get; set; }
        public virtual DbSet<ContactInfo> ContactInfos { get; set; }
    }
}