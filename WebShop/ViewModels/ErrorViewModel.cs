using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {
        public HandleErrorInfo eInfo { get; set; }
    }
}