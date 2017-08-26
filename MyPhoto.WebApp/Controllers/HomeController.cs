
using MyEvernote.Common.Helpers;
using MyPhoto.WebApp.Filter;
using MyPhotos.BusinessLayer;
using MyPhotos.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MyPhoto.WebApp.Controllers
{
    
    public class HomeController : Controller
    {
        PhotoManager pm = new PhotoManager();
        CategoryManager cm = new CategoryManager();
        UserManager user = new UserManager();

        ExcepClass except = new ExcepClass();



        // GET: Home
        public ActionResult Index()
        {

            List<Category> cat = cm.Select();

            return View(cat.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        // GET: Photo
        public ActionResult CategorysPhoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Category category = cm.Find(x => x.Id == id);

            if (category == null) { return HttpNotFound(); }

            return View("CategorysPhoto", category.Photos.OrderByDescending(x => x.ModifiedOn).ToList());
        }
  
        public ActionResult MostLiked()
        {


            List<Photo> photo = pm.GetPhoto().OrderByDescending(x => x.LikeCount).ToList();

            return View("CategorysPhoto", photo);


        }
       
        public ActionResult Last()
        {


            List<Photo> photo = pm.GetPhoto().OrderByDescending(x => x.ModifiedOn).ToList();

            return View("CategorysPhoto", photo);


        }

        public ActionResult About()
        {


            return View();


        }
     
        public ActionResult Login()
        {


            return View();


        }

        [HttpPost]
        [Errors]
        public ActionResult Login(LoginViewModel login)
        {



            except.LoginExcep(login);


            BusinessLayerResult<MyPhotosUser> checkuser = user.LoginUser(login);

            if (checkuser.Errors.Count > 0)
            {
                foreach (var item in checkuser.Errors)
                {
                    ModelState.AddModelError("", item.Message);

                    return View(login);
                }
            }

            Session["login"] = checkuser.Result;

            return RedirectToAction("Index");

            //}

            //return View();
        }
        
        public ActionResult Register()
        {


            return View();

        }

        [HttpPost]
        [Errors]

        public ActionResult Register(RegisterView reg)
        {

            except.RegisterExcep(reg);


            BusinessLayerResult<MyPhotosUser> buser = user.RegisterUser(reg);

            if (buser.Errors.Count > 0)
            {
                foreach (var item in buser.Errors)
                {
                    ModelState.AddModelError("", item.Message);
                }

                return View(reg);
            }



            else

                return RedirectToAction("RedirectRegister");



        }

      
        public ActionResult RedirectRegister()
        {
            return View();
        }
     
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index");

        }
    
        public ActionResult Activate(Guid id)
        {



            BusinessLayerResult<MyPhotosUser> ruser = user.ActivateUser(id);

            if (ruser.Errors.Count > 0)
            {
                TempData["error"] = ruser.Errors;

                return RedirectToAction("ActivateError");
            }



            return RedirectToAction("ActivateOK");
        }
       
        public ActionResult ActivateError()
        {
            List<ErrorCodeObj> error = null;

            if (TempData["error"] != null)
            {
                error = TempData["error"] as List<ErrorCodeObj>;
            }

            return View(error);


        }
    
        public ActionResult ActivateOK()
        {
            return View();
        }
        [Aut]
        public ActionResult ShowProfile()
        {
            MyPhotosUser currentuser = Session["login"] as MyPhotosUser;



            BusinessLayerResult<MyPhotosUser> blu = user.GetById(currentuser.Id);

            if (blu.Errors.Count > 0)
            {
                //yönledir
            }

            return View(blu.Result);

        }



        [Aut]
        public ActionResult EditProfile()
        {
            MyPhotosUser currentuser = Session["login"] as MyPhotosUser;



            BusinessLayerResult<MyPhotosUser> blu = user.GetById(currentuser.Id);

            if (blu.Errors.Count > 0)
            {
                //yönledir
            }

            return View(blu.Result);
        }
        [Aut]
        public ActionResult RemoveProfile()
        {
            MyPhotosUser currentuser = Session["login"] as MyPhotosUser;



            BusinessLayerResult<MyPhotosUser> blu = user.RemoveUser(currentuser.Id);

            if (blu.Errors.Count > 0)
            {
                foreach (var item in blu.Errors)


                    ModelState.AddModelError("", item.Message);
            }

            Session.Clear();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Errors]
        [Aut]
        public ActionResult EditProfile(MyPhotosUser Model)
        {

            except.EditProfileExcep(Model);

            BusinessLayerResult<MyPhotosUser> ruser = user.UpdateProfile(Model);

            if (ruser.Errors.Count > 0)
            {
                foreach (var item in ruser.Errors)
                {
                    ModelState.AddModelError("", item.Message);


                }

                return View(Model);
            }

            Session["login"] = ruser.Result;
            return RedirectToAction("ShowProfile");




        }

       
    }
}