using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class BaseLayerController : Controller
    {
        public FooterViewModel GetFooter()
        {
            //Footer Data
            FooterViewModel FooterData = new FooterViewModel();
            FooterData.CompanyName = "Sana Webshop";//Can be set to dynamic value
            FooterData.Year = DateTime.Now.Year.ToString();
            return FooterData;
        }

        public HeaderViewModel GetHeader()
        {
            //Header Data
            HeaderViewModel HeaderData = new HeaderViewModel();
            HeaderData.WebsiteTitle = "Sana Webshop";
            HeaderData.Tagline = "Product Management Demo";

            return HeaderData;
        }
    }
}