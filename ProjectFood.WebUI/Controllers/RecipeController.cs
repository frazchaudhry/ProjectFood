using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectFood.Domain.Abstract;
using ProjectFood.Domain.Entities;
using ProjectFood.WebUI.Hubs;
using ProjectFood.WebUI.Infrastructure;
using ProjectFood.WebUI.Models;

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
        public ViewResult Index(string searchString = null, int categoryId = 0,
            int regionId = 0, bool isVegetarian = false, string ingredients = null)
        {
            var recipes = unitOfWork.RecipeRepository.Get();

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.RecipeName.ToUpper().Contains(searchString.ToUpper()));
            }
            if (categoryId > 0)
            {
                recipes = recipes.Where(r => r.CategoryId == categoryId).ToList();
            }
            if (regionId > 0)
            {
                recipes = recipes.Where(r => r.RegionId == regionId).ToList();
            }
            if (isVegetarian)
            {
                recipes = recipes.Where(r => r.IsVegetarian);
            }
            if (!String.IsNullOrEmpty(ingredients))
            {
                var ingredientList = ingredients.Split(new [] {","} , StringSplitOptions.None);
                recipes = ingredientList.Aggregate(recipes, (current, ingredient) => current.Where(r => !String.IsNullOrEmpty(r.Ingredients) && r.Ingredients.Contains(ingredient)));
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

        [HttpPost]
        public async Task<ActionResult> Details(Comment comment)
        {
            if (ModelState.IsValid)
            {
                var recipe = unitOfWork.RecipeRepository.GetById(comment.RecipeId);
                if (recipe == null) return RedirectToAction("Index");

                comment.CreateDateTime = DateTime.Now;
                recipe.Comments.Add(comment);
                unitOfWork.RecipeRepository.Update(recipe);
                unitOfWork.Save();
                var user = await IdentityHelpers.UserManager.FindByIdAsync(comment.UserId);
                var jsonComment = new CommentJsonViewModel
                {
                    UserId = comment.UserId,
                    UserName = user.UserName,
                    CommentText = comment.CommentText,
                    CreateDateTime = String.Format("{0:r}", comment.CreateDateTime),
                };
                return Json(jsonComment);
            }

            return RedirectToAction("Index");
        }

        [System.Web.Mvc.Authorize]
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
        [System.Web.Mvc.Authorize]
        [HttpPost, ValidateInput(false)]
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
        [System.Web.Mvc.Authorize]
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
        [HttpPost, ValidateInput(false)]
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

        public JsonResult GetRecipeCommentsForUser(int recipeId)
        {
            var recipe = unitOfWork.RecipeRepository.GetById(recipeId);
            if (recipe != null)
            {
                var comments = recipe.Comments;
                var commentsJson = comments.Select(comment => new CommentJsonViewModel
                {
                    UserId = comment.UserId, UserName = IdentityHelpers.GetUserName(comment.UserId), CommentText = comment.CommentText, CreateDateTime = String.Format("{0:r}", comment.CreateDateTime)
                }).ToList();
                return Json(commentsJson);
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
