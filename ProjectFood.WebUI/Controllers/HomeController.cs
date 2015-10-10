using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFood.Domain.Abstract;

namespace ProjectFood.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Home
        public ViewResult Index()
        {
            var recipes = unitOfWork.RecipeRepository.Get().ToList();
            return View(recipes);
        }
    }
}