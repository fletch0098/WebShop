using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        //Default 
        public ActionResult Index()
        {
            //Redirect to Session DB default
            return RedirectToAction("SessionDb");
        }
      
        //set view with Database data
        public ActionResult MsSQLDb()
        {
            //Create a new view model
            ProductListViewModel vm = new ProductListViewModel();

            //Save the Db preferences in session
            Session["DataBase"] = "MsSQL";

            //Bind the view model with Data
            vm = BindListView("MsSQL");

            //Save the Db prefference in view model
            vm.Database = "MsSQL";

            //Return the View
            return View("Index", vm);
        }

        //Set view with session data
        public ActionResult SessionDb()
        {
            //Create a new view model
            ProductListViewModel vm = new ProductListViewModel();

            //Save the Db Preferences in session
            Session["DataBase"] = "Session";

            //Bind the view model with data
            vm = BindListView("Session");

            //Save the Db prefference in view model
            vm.Database = "Session";

            //Return the View
            return View("Index", vm);
        }

        //Add a new product
        public ActionResult AddProduct()
        {
            //Create a new view model
            CreateProductViewModel vm = new CreateProductViewModel();

            //Save the Db preferences in the view model
            vm.Database = Session["Database"].ToString();

            //Return the view
            return View("CreateProduct", vm);
        }

        //Save a new product
        public ActionResult SaveProduct(Product p)
        {
            //if product is valid against entity model(server side)
            if (ModelState.IsValid)
                    {
                        //Check Db preferences
                        if (Session["DataBase"].ToString() == "Session")
                        {
                            //Retrieve current products
                            List<Product> Products = GetSessionProducts();

                            //Add new product to list
                            Products.Add(p);
                            //Store back into session
                            Session["Products"] = Products;
                            //Return to product list view
                            return RedirectToAction("SessionDb");
                        }
                        else
                        {
                            //Using database storage
                            ProductBusinessLayer empBal = new ProductBusinessLayer();
                            //Add to entity list
                            empBal.SaveProduct(p);
                            //Return to product list view
                            return RedirectToAction("MsSQLDb");
                        }
                    }
            else
                    {
                        //Does not pass Server validation return to view with state
                        //create new view model
                        CreateProductViewModel vm = new CreateProductViewModel();

                        //return to state
                        vm.Title = p.Title;
                        vm.ProductNumber = p.ProductNumber;
                        vm.Database = Session["Database"].ToString();

                //state of integer default
                if (p.Price == 0)
                        {
                            vm.Price = p.Price.ToString();
                        }
                else
                        {
                            vm.Price = ModelState["Price"].Value.AttemptedValue;
                        }

                //Return to view with state
                return View("CreateProduct", vm);
             }
            
        }

        //Bind view data
        private ProductListViewModel BindListView(string database)
        {
            //Create new list view model
            ProductListViewModel vm = new ProductListViewModel();

            //Create new list of products
            List<Product> Products = new List<Product>();

            //Check Db Preference
            if (database == "Session")
            {
                //fill view model with Session data
                Products = GetSessionProducts();
            }
            else
            {
                //fill view model with database data
                ProductBusinessLayer empBal = new ProductBusinessLayer();
                Products = empBal.GetProducts();
            }

            //layer in case of any changes to the view
            //Create new product view model
            List<ProductViewModel> empViewModels = new List<ProductViewModel>();
            
            //set up product view model
            foreach (Product prd in Products)
            {
                ProductViewModel prdViewModel = new ProductViewModel();
                prdViewModel.Title = prd.Title;
                prdViewModel.ProductNumber = prd.ProductNumber;
                prdViewModel.Price = prd.Price.ToString("C");
                empViewModels.Add(prdViewModel);
            }

            //set list
            vm.Products = empViewModels;

            //return the binded vm
            return vm;
        }

        //Retrieve session data
        private List<Product> GetSessionProducts()
        {
            //Empty list
            List<Product> Products = new List<Product>();

            //Check for session data
            if (Session["Products"] != null)
            {
                //fill list
                Products = Session["Products"] as List<Product>;
            }

            //return data
            return Products;

        }

    }
}