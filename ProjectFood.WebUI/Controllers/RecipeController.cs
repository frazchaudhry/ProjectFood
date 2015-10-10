using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectFood.Domain.Abstract;
using ProjectFood.Domain.Concrete;
using ProjectFood.Domain.Entities;

namespace ProjectFood.WebUI.Controllers
{
    public class RecipeController : Controller
    {
        private IUnitOfWork unitOfWork;

        public RecipeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Recipe
        public ActionResult Index(string searchString = null)
        {
            var recipes = unitOfWork.RecipeRepository.Get();

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.RecipeName.ToUpper().Contains(searchString.ToUpper()));
            }

            
            return View(recipes.ToList());
        }

        // GET: Recipe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = unitOfWork.RecipeRepository.GetById((int)id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipe/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.Get().ToList(), "CategoryId", "CategoryName");
            ViewBag.RegionId = new SelectList(unitOfWork.RegionRepository.Get().ToList(), "RegionId", "RegionName");
            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeId,RecipeName,RecipeDescription,Directions,IsVegetarian,RegionId,CategoryId,ImageData,ImageMimeType,UserId,Likes,Dislikes,CreateDateTime,UpdateDatetime")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RecipeRepository.Insert(recipe);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.Get().ToList(), "CategoryId", "CategoryName", recipe.CategoryId);
            ViewBag.RegionId = new SelectList(unitOfWork.RegionRepository.Get().ToList(), "RegionId", "RegionName", recipe.RegionId);
            return View(recipe);
        }

        // GET: Recipe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = unitOfWork.RecipeRepository.GetById((int)id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.Get().ToList(), "CategoryId", "CategoryName", recipe.CategoryId);
            ViewBag.RegionId = new SelectList(unitOfWork.RegionRepository.Get().ToList(), "RegionId", "RegionName", recipe.RegionId);
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeId,RecipeName,RecipeDescription,Directions,IsVegetarian,RegionId,CategoryId,ImageData,ImageMimeType,UserId,Likes,Dislikes,CreateDateTime,UpdateDatetime")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.RecipeRepository.Update(recipe);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.Get().ToList(), "CategoryId", "CategoryName", recipe.CategoryId);
            ViewBag.RegionId = new SelectList(unitOfWork.RegionRepository.Get().ToList(), "RegionId", "RegionName", recipe.RegionId);
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = unitOfWork.RecipeRepository.GetById((int)id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.RecipeRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int recipeId)
        {
            Recipe recipe = unitOfWork.RecipeRepository.Get().FirstOrDefault(r => r.RecipeId == recipeId);
            if (recipe != null)
            {
                return File(recipe.ImageData, recipe.ImageMimeType);
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
