using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.DataAccessLayer;
using System.Web.SessionState;

namespace WebShop.Models
{
    public class ProductBusinessLayer
    {
        //Return a list of Products
        public List<Product> GetProducts()
        {
            //Get Session
            HttpContext context = HttpContext.Current;

            if (!UsingSessionDb())
            {
                //Persistant Storage
                WebShopERPDAL salesDal = new WebShopERPDAL();
                return salesDal.Products.ToList();
            }
            else
            {
                //Memory Storae
                return GetSessionProducts();
            }
            
        }

        //Save a product
        public Product SaveProduct(Product p)
        {
            //Get Session
            HttpContext context = HttpContext.Current;

            if (!UsingSessionDb())
            {
                //Persistant Storage
                WebShopERPDAL salesDal = new WebShopERPDAL();
                salesDal.Products.Add(p);
                salesDal.SaveChanges();
                return p;
            }
            else
            {
                //Memory Storage
                //Get the next key
                p.ProductID = NextSessionKey();

                //Retrieve current products
                List<Product> Products = GetSessionProducts();

                //Add new product to list
                Products.Add(p);

                //Store back into session
                context.Session["Products"] = Products;
                SetSessionKey(p.ProductID.ToString());
                return p;
            }
        }

        //Delete product with id
        public Product DeleteProduct(int id)
        {
            //Get Session
            HttpContext context = HttpContext.Current;

            //Look up product, then delete
            Product p = GetProduct(id);

            if (!UsingSessionDb())
            {
                WebShopERPDAL salesDal = new WebShopERPDAL();
                salesDal.Products.Attach(p);
                salesDal.Products.Remove(p);
                salesDal.SaveChanges();
                return p;
            }
            else
            {
                //Retrieve current products
                List<Product> Products = GetSessionProducts();

                //Remove product from list
                Products.Remove(p);

                //Store back into session
                context.Session["Products"] = Products;
                return p;
            }
        }

        //Retrieve session data
        public List<Product> GetSessionProducts()
        {
            //Empty list
            List<Product> Products = new List<Product>();

            //Get Session
            HttpContext context = HttpContext.Current;

            //Check for session data
            if (context.Session["Products"] != null)
            {
                //fill list
                Products = context.Session["Products"] as List<Product>;
            }

            //return data
            return Products;

        }

        //Get a Product
        private Product GetProduct(int id)
        {

            //Check Db preferences
            if (UsingSessionDb())
            {
                //Retrieve current products 
                List<Product> Products = GetSessionProducts();

                //find in list
                return Products.Find(p => p.ProductID == id);

            }
            else
            {
                //Return MsSQL Data
                WebShopERPDAL salesDal = new WebShopERPDAL();
                return salesDal.Products.Find(id);
            }

        }

        //Retreive the next session key
        private int NextSessionKey()
        {
            //Get Session
            HttpContext context = HttpContext.Current;

            //Check if session data
            if (context.Session["LastSessionKey"] != null)
            {
                //Increment and return
                return (Convert.ToInt32(context.Session["LastSessionKey"])) + 1;
            }
            else
            {
                //default
                return 0;
            }
        }

        //Update last used key
        private void SetSessionKey(string lastsessionkey)
        {
            //Get Session
            HttpContext context = HttpContext.Current;

            //Update Session key
            context.Session["LastSessionKey"] = lastsessionkey;
        }

        //Using Session?
        private bool UsingSessionDb()
        {
            //Get Session
            HttpContext context = HttpContext.Current;


            if (context.Session["DataBase"].ToString() == "MsSQL")
            { return false; }
            else
            { return true; }
        }

    }
}