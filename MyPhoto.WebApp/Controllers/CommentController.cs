using MyPhotos.BusinessLayer;
using MyPhotos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPhoto.WebApp.Models;
using MyPhoto.WebApp.Filter;

namespace MyPhoto.WebApp.Controllers
{
    public class CommentController : Controller
    {
        PhotoManager photo = new PhotoManager();
        CommentManager comment = new CommentManager();
    
        // GET: Comment
        public ActionResult ShowPhotoComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo1 = photo.Find(x => x.Id == id);

            if (photo1 == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialCommentView",photo1.Comments);
        }
        [HttpPost]
        public ActionResult Edit(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment1=comment.Find(x => x.Id == id);

            comment1.Text = text;
            comment1.ModifiedOn = DateTime.Now;

           

            if (comment.Update(comment1) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);


        }

        [Aut]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment1 = comment.Find(x => x.Id == id);

          

            
            if (comment.Delete(comment1)>0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);


        }

        [Aut]
        public ActionResult Create(Comment com, int? photoid)
        {
            if (photoid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            com.Owner = CurrentSession.User;
            
            com.Photo = photo.Find(x => x.Id == photoid);


            if (comment.Insert(com) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);




        }

    }
}