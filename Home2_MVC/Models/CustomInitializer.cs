using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;

namespace Home2_MVC.Models
{
    internal class CustomInitializer<T> : DropCreateDatabaseAlways<Model>
    {
        protected override void Seed(Model _ctx)
        {
            _ctx.Products.AddRange(new List<Product>
            {
                new Product("Paperoni", 70, "Pepperoni sausages, mozzarella cheese, olive oil, basil, tomato sauce", "https://images.pizza33.ua/products/product/POCpLYdcgVA34bcde4pK8JEjSWITKbtk.jpg"),
                new Product("Chipollo", 80, "Pepperoni sausages, mozzarella cheese, olive oil, basil, tomato sauce", "https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg"),
                new Product("Diablo", 90, "Pepperoni sausages, mozzarella cheese, olive oil, basil, tomato sauce", "https://images.pizza33.ua/products/product/yQfkJqZweoLn9omo68oz5BnaGzaIE0UJ.jpg")
            });
            _ctx.SaveChanges();
        }
    }
}