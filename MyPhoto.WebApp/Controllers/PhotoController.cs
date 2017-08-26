using MyPhoto.WebApp.Filter;
using MyPhoto.WebApp.Models;
using MyPhotos.BusinessLayer;
using MyPhotos.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyPhoto.WebApp.Controllers
{
    public class PhotoController : Controller
    {
        private PhotoManager photomanager = new PhotoManager();
        private CategoryManager categoryManager = new CategoryManager();
        private LikesManager likedManager = new LikesManager();

        [Aut]
        public ActionResult Index()
        {

          var photos = photomanager.ListQueryable().Include("Category").Include("Owner").Where(x => x.Owner.Id == CurrentSession.User.Id).OrderByDescending(x => x.ModifiedOn);
         

            return View(photos.ToList());
        }


        [Aut]
        public ActionResult MyLikedPhotos()
        {

            var like = likedManager.ListQueryable().Where(x => x.LikedUser.Id == CurrentSession.User.Id).Select(x => x.Photo).OrderByDescending(x => x.ModifiedOn);

            return View(like.ToList());

        }

        [Aut]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo=photomanager.Find(x => x.Id == id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        [Aut]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categoryManager.Select(), "Id", "Title");
            return View();
        }

      
        [HttpPost]
        [Aut]
        public ActionResult Create(Photo photo, HttpPostedFileBase photofile)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            photo.Owner = CurrentSession.User;

            if (photofile != null &&


                (photofile.ContentType == "image/jpg" || photofile.ContentType == "image/jpeg" || photofile.ContentType == "image/png"))

            {

                string FileName = $"{photo.Title}.{photofile.ContentType.Split('/')[1]}";
                photofile.SaveAs(Server.MapPath($"Content/Photo/{FileName}"));

              photo.PhotoPath = $"/Content/Photos/{FileName}";
            }
            ModelState.Remove("Id");


            if (ModelState.IsValid)
            {
                photomanager.Insert(photo);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(categoryManager.Select(), "Id", "Title", photo.CategoryID);

            return View(photo);
        }

    
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photos =photomanager.Find(x => x.Id == id);
            if (photos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(categoryManager.Select(), "Id", "Title", photos.CategoryID);
            return View(photos);
        }

   
        [HttpPost]
 
        public ActionResult Edit(Photo photo, HttpPostedFileBase file)
        {
            //ModelState.Remove("CreatedOn");
            //ModelState.Remove("ModifiedOn");
            //ModelState.Remove("ModifiedUsername");

            if (file != null &&


               (file.ContentType == "image/jpg" || file.ContentType == "image/jpeg" || file.ContentType == "image/png"))

            {

                string FileName = $"{photo.Title}.{file.ContentType.Split('/')[1]}";
                file.SaveAs(Server.MapPath($"/Content/Photos/{FileName}"));

                photo.PhotoPath = $"/Content/Photos/{FileName}";
            }
            ModelState.Remove("Id");


            if (ModelState.IsValid)

            {
                photo.Owner = CurrentSession.User;

                Photo photos =photomanager.Find(x => x.Id == photo.Id);

                photos.CategoryID = photo.CategoryID;
               
                photos.Title =photo.Title;

                photomanager.Update(photos);

                return RedirectToAction("Index");
            }

           


            ViewBag.CategoryId = new SelectList(categoryManager.Select(), "Id", "Title", photo.CategoryID);

            return View(photo);
        }


    
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            

            Photo photos = photomanager.Find(x => x.Id == id);
            if (photos == null)
            {
                return HttpNotFound();
            }
            return View(photos);
        }

     
        [HttpPost, ActionName("Delete")]
   
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photos = photomanager.Find(x => x.Id == id);
            photomanager.Delete(photos);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult GetLiked(int[] ids)
        {
            if (CurrentSession.User != null)
            {
                List<int> likedPhotoIds = likedManager.List(
                    x => x.LikedUser.Id == CurrentSession.User.Id && ids.Contains(x.Photo.Id)).Select(
                    x => x.Photo.Id).ToList();

                return Json(new { result = likedPhotoIds });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }
        }

        [HttpPost]
        public ActionResult SetLikeState(int photoid, bool liked)
        {
            int res = 0;

            Liked like= likedManager.Find(x => x.Photo.Id == photoid && x.LikedUser.Id == CurrentSession.User.Id);

            Photo photo = photomanager.Find(x => x.Id == photoid);

            if (like != null && liked==false)
            {

                res = likedManager.Delete(like);
                
            }

            else if(like==null && liked==true)
            {
                res = likedManager.Insert(new Liked()
                {

                    LikedUser = CurrentSession.User,
                    Photo = photo


                });

            }

            if (res > 0)
            {
                if (liked)
                {
                    photo.LikeCount++;
                }

                else
                {
                    photo.LikeCount--;
                }

                res = photomanager.Update(photo);

                return Json(new { hasError = false, errormassege = string.Empty, result = photo.LikeCount });
            }

            return Json(new { hasError = true, errormassege = "Beğenme işlemi gerçekleştrilemedi.", result = photo.LikeCount });


        }


        public ActionResult GetPhotoDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo1=photomanager.Find(x => x.Id == id);

            if (photo1 == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialPhotoDetails", photo1);
        }
    }
}

