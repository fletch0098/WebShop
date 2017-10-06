using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
    public class BaseViewModel
    {
        public HeaderViewModel HeaderData { get; set; }
        public FooterViewModel FooterData{get; set;}
    }
}