using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProjectFood.Domain.Abstract;
using ProjectFood.Domain.Concrete;
using ProjectFood.Domain.Entities;
using ProjectFood.WebUI.Infrastructure;

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
        public ActionResult Index(string searchString = null, int categoryId = 0)
        {
            var recipes = unitOfWork.RecipeRepository.Get();

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.RecipeName.ToUpper().Contains(searchString.ToUpper()));
            }
            else if (categoryId > 0)
            {
                recipes = recipes.Where(r => r.CategoryId == categoryId).ToList();
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

        [Authorize]
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Recipe recipe, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    recipe.ImageMimeType = image.ContentType;
                    recipe.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(recipe.ImageData, 0, image.ContentLength);
                }
                recipe.CreateDateTime = DateTime.Now;
                recipe.UpdateDatetime = DateTime.Now;

                var user = await IdentityHelpers.UserManager.FindByNameAsync(User.Identity.Name);
                recipe.UserId = user.Id;

                unitOfWork.RecipeRepository.Insert(recipe);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(unitOfWork.CategoryRepository.Get().ToList(), "CategoryId", "CategoryName", recipe.CategoryId);
            ViewBag.RegionId = new SelectList(unitOfWork.RegionRepository.Get().ToList(), "RegionId", "RegionName", recipe.RegionId);
            return View(recipe);
        }

        // GET: Recipe/Edit/5
        [Authorize]
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
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Recipe recipe, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    recipe.ImageMimeType = image.ContentType;
                    recipe.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(recipe.ImageData, 0, image.ContentLength);
                }
                recipe.UpdateDatetime = DateTime.Now;
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
