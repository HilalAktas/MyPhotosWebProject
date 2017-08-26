using MyPhotos.Common;
using MyPhotos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPhoto.WebApp
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUserName()
        {
           if(HttpContext.Current.Session["login"] != null){

                MyPhotosUser user = HttpContext.Current.Session["login"] as MyPhotosUser;
                return user.UserName;
            }
              return "system";

        }
    }
}