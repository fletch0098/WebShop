using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Filters;
using WebShop.ViewModels;

namespace WebShop.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HeaderFooterFilter]
        public ActionResult Index()
        {
            ErrorViewModel evm = new ErrorViewModel();

            Exception e = new Exception("Invalid Controller or/and Action Name");
            evm.eInfo = new HandleErrorInfo(e, "Unknown", "Unknown");
            return View("Error", evm);
        }
    }
}