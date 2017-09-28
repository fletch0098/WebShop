using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductNumber { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }

    }

}