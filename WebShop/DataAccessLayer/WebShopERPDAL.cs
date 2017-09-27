using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebShop.Models;

namespace WebShop.DataAccessLayer
{
    public class WebShopERPDAL : DbContext
    {

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("TblProducts");
            base.OnModelCreating(modelBuilder);
        }
    }
}