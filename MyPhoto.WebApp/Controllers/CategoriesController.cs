using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using MyPhotos.Entities;
using MyPhotos.BusinessLayer;
using MyPhoto.WebApp.Filter;

namespace MyPhoto.WebApp.Controllers
{   [Aut]
    [AdminFilter]
    public class CategoriesController : Controller
    {
        CategoryManager cat = new CategoryManager();
 
  
        public ActionResult Index()
        {
            return View(cat.Select());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cat.Find(x => x.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
           

            return View();
            
        }

       
       [HttpPost]
        public ActionResult Create (Category category, HttpPostedFileBase CategoryPhoto)
        {
            if (CategoryPhoto != null &&


               (CategoryPhoto.ContentType == "image/jpg" || CategoryPhoto.ContentType == "image/jpeg" || CategoryPhoto.ContentType == "image/png"))

            {

                string FileName = $"{category.Id}.{CategoryPhoto.ContentType.Split('/')[1]}";
                CategoryPhoto.SaveAs(Server.MapPath($"/Content/Photos/{FileName}"));

                category.CategoryPhoto = $"/Content/Photos/{FileName}";
            }
            ModelState.Remove("Id");


            if (ModelState.IsValid)
            {
                cat.Insert(category);
              
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cat.Find(x => x.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category, HttpPostedFileBase CategoryPhoto)
        {
            if (CategoryPhoto != null && 
                
                
               ( CategoryPhoto.ContentType=="image/jpg"|| CategoryPhoto.ContentType=="image/jpeg"||CategoryPhoto.ContentType=="image/png" ))
                
            {

                string FileName = $"{category.Id}.{CategoryPhoto.ContentType.Split('/')[1]}";
                CategoryPhoto.SaveAs(Server.MapPath($"/Content/Photos/{FileName}"));

                category.CategoryPhoto = $"/Content/Photos/{FileName}";
            }
            
            if (ModelState.IsValid)
            {
                Category obj = cat.Find(x => x.Id == category.Id);

                obj.Description = category.Description;
                obj.Title = category.Title;
                obj.CategoryPhoto = category.CategoryPhoto;
                
                cat.Update(obj);

                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = cat.Find(x => x.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = cat.Find(x => x.Id == id);
            cat.Delete(category);

            return RedirectToAction("Index");
        }

    
    }
}
