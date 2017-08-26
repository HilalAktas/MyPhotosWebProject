using MyPhotos.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MyPhoto.WebApp
{
    public class Errors : FilterAttribute, IExceptionFilter
    {
       

        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.TempData["LastError"] = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            if (ExcepClass.code == null)
            {
                filterContext.Result = new RedirectResult("/Home/Error");
            }

            if (ExcepClass.code==ExceptionCode.Login)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }

            if (ExcepClass.code == ExceptionCode.Register)
            {
                filterContext.Result = new RedirectResult("/Home/Register");
            }

            if (ExcepClass.code == ExceptionCode.EditProfile)
            {
                filterContext.Result = new RedirectResult("/Home/EditProfile");
            }

        }
    }
}