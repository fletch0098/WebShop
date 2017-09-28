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
                //Return the View
                return View("Index", BindProductListViewModel());
        }
      
        //set view with Database data
        public ActionResult MsSQLDb()
        {
            //Update session
            SetSessionDatabase("MsSQL");
            
            //Return the View
            return RedirectToAction("Index");
        }

        //Set view with session data
        public ActionResult SessionDb()
        {
            //Update Session
            SetSessionDatabase("Session");

            //Return the View
            return RedirectToAction("Index");
        }

        //set new product view
        [HttpGet]
        public ActionResult AddProduct()
        {
            //Create a new view model
            CreateProductViewModel vm = new CreateProductViewModel();

            //Save the Db preferences in the view model
            vm.Database = GetSessionDatabase();

            //Return the view
            return View("AddProduct", vm);
        }

        //Save a new product
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            //if product is valid against entity model(server side)
            if (ModelState.IsValid)
            {
                //Using database storage
                ProductBusinessLayer empBal = new ProductBusinessLayer();

                //Add to entity list
                empBal.SaveProduct(p);

                //Return to product list view
                return RedirectToAction("Index");
            }
            else
            {
                //Does not pass Server validation return to view with state
                //create new view model
                CreateProductViewModel vm = new CreateProductViewModel();

                //return to state
                vm.Title = p.Title;
                vm.ProductNumber = p.ProductNumber;
                vm.Database = GetSessionDatabase();

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
                return View("AddProduct", vm);
            }
            
        }

        //Delete product
        public ActionResult DeleteProduct(int id)
        {
            //Using database storage
            ProductBusinessLayer empBal = new ProductBusinessLayer();

            //Add to entity list
            empBal.DeleteProduct(id);

            //Return to previous view
            return RedirectToAction("Index");
        }

        //Bind the view
        private ProductListViewModel BindProductListViewModel()
        {
            //Create a new view model
            ProductListViewModel vm = new ProductListViewModel();

            //Get Database Preferences
            string database = GetSessionDatabase();

            //Create new list of products
            List<Product> Products = new List<Product>();

            //fill view model with data
            ProductBusinessLayer empBal = new ProductBusinessLayer();
            Products = empBal.GetProducts();

            //Create new product view model
            List<ProductViewModel> empViewModels = new List<ProductViewModel>();

            //set up product view model
            foreach (Product prd in Products)
            {
                ProductViewModel prdViewModel = new ProductViewModel();
                prdViewModel.Title = prd.Title;
                prdViewModel.ProductNumber = prd.ProductNumber;
                prdViewModel.Price = prd.Price.ToString("C");
                prdViewModel.ProductId = prd.ProductID;
                empViewModels.Add(prdViewModel);
            }

            //set list
            vm.Products = empViewModels;

            //Save the Db prefference in view model
            vm.Database = database;

            //Return the View
            return vm;
        }

        //Bool for Session Db
        private bool UsingSessionDb()
        {
            if (Session["DataBase"] != null)
            {
                //Not Null check which
                if (Session["DataBase"].ToString() == "MsSQL")
                    //Not Session
                { return false; }
                else
                //Session
                { return true; }

            }
            else
            {
                //Defualt
                SetSessionDatabase("Session");
                { return true; }
            }
            
            
        }

        //Update Session
        private void SetSessionDatabase(string db)
        {
            Session["Database"] = db;
        }

        //Return which database
        private string GetSessionDatabase()
        {
            if (Session["Database"] != null)
            {
                //Return session data
                return Session["Database"].ToString();
            }
            {
                //Default
                SetSessionDatabase("Session");
                return "Session";
            }
        }

    }
}