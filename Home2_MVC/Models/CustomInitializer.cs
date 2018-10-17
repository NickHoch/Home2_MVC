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
            _ctx.Users.Add(new User { Login = "admin", Password = Hash.HashPassword("admin"), IsAdmin = true });
            _ctx.Users.Add(new User { Login = "auditor", Password = Hash.HashPassword("auditor"), IsAdmin = false });
            _ctx.Products.AddRange(new List<Product>
            {
                new Product(1, "Pizza Ukrainian"),
                new Product(2, "Pizza Hawaiian"),
                new Product(3, "Pizza Milano")
            });
            _ctx.SaveChanges();
        }
    }
    public static class Hash
    {
        public static string HashPassword(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}