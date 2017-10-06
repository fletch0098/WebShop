using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
    public class CreateProductViewModel : BaseViewModel
    {
        public string ProductNumber { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Database { get; set; }
    }
}