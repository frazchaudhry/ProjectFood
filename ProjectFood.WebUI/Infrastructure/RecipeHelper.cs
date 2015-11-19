using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFood.WebUI.Infrastructure
{
    public static class RecipeHelper
    {
        public static List<string> GetIngredientList(string rawIngredients)
        {
            var ingredients = rawIngredients.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            return ingredients.ToList();
        }
    }
}