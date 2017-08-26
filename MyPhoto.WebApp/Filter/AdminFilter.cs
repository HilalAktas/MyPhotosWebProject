using MyPhoto.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPhoto.WebApp.Filter
{
    public class AdminFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if ( CurrentSession.User != null  && CurrentSession.User.IsAdmin == false)
            {

                filterContext.Result = new RedirectResult("/Home/Index");

            }
        }
    }
}