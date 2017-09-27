using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebShop.ViewModels
{
    public class ProductListViewModel
    {

        public List<ProductViewModel> Products { get; set; }
        public string Database { get; set; }

    }
}