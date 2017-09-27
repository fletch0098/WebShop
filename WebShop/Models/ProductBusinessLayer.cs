using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.DataAccessLayer;

namespace WebShop.Models
{
    public class ProductBusinessLayer
    {
        public List<Product> GetProducts()
        {
            WebShopERPDAL salesDal = new WebShopERPDAL();
            return salesDal.Products.ToList();
        }

        public Product SaveProduct(Product p)
        {
            WebShopERPDAL salesDal = new WebShopERPDAL();
            salesDal.Products.Add(p);
            salesDal.SaveChanges();
            return p;
        }

        public Product DeleteProduct(Product p)
        {
            WebShopERPDAL salesDal = new WebShopERPDAL();
            salesDal.Products.Remove(p);
            salesDal.SaveChanges();
            return p;
        }

    }
}