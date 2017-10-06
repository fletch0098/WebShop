using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.ViewModels;
using WebShop.Controllers;

namespace WebShop.Filters
{
    public class HeaderFooterFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult v = filterContext.Result as ViewResult;
            if (v != null) // v will null when v is not a ViewResult
            {
                BaseViewModel bvm = v.Model as BaseViewModel;
                if (bvm != null)//bvm will be null when we want a view without Header and footer
                {
                    //Footer Data
                    BaseLayerController baseLayerController = new BaseLayerController();
                    bvm.FooterData = baseLayerController.GetFooter();
                    bvm.HeaderData = baseLayerController.GetHeader();
                }
            }
        }
    }
}