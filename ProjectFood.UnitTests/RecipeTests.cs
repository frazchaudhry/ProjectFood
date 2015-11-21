using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProjectFood.Domain.Abstract;
using ProjectFood.Domain.Entities;
using ProjectFood.WebUI.Controllers;

namespace ProjectFood.UnitTests
{
    [TestClass]
    public class RecipeTests
    {
        [TestMethod]
        public void GetVegetarianRecipes()
        {
            // Arrange
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(r => r.RecipeRepository.Get(null, null, "")).Returns(new Recipe[]
            {
                new Recipe { RecipeId = 1, RecipeName = "Recipe1", IsVegetarian = true},
                new Recipe { RecipeId = 2, RecipeName = "Recipe2", IsVegetarian = false},
                new Recipe { RecipeId = 3, RecipeName = "Recipe3", IsVegetarian = false},
                new Recipe { RecipeId = 4, RecipeName = "Recipe4", IsVegetarian = true}
            });

            // Arrange
            RecipeController target = new RecipeController(mock.Object);

            // Act - get the model from the controller
            var isVegetarian = true;
            var result = ((IEnumerable<Recipe>)target.Index(null, 0, 0, isVegetarian).Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].IsVegetarian, true);
            Assert.AreEqual(result[1].IsVegetarian, true);
        }

        [TestMethod]
        public void FilterByIngredients()
        {
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(r => r.RecipeRepository.Get(null, null, "")).Returns(new Recipe[]
            {
                new Recipe { RecipeId = 1, RecipeName = "Recipe1", IsVegetarian = true, Ingredients = "beef, chicken"},
                new Recipe { RecipeId = 2, RecipeName = "Recipe2", IsVegetarian = false, Ingredients = "peppers, chicken"},
                new Recipe { RecipeId = 3, RecipeName = "Recipe3", IsVegetarian = false},
                new Recipe { RecipeId = 4, RecipeName = "Recipe4", IsVegetarian = true}
            });

            // Arrange
            RecipeController target = new RecipeController(mock.Object);

            // Act - get the model from the controller
            var result = ((IEnumerable<Recipe>)target.Index(null, 0, 0, false, "peppers").Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 1);
            Assert.AreEqual(result[0].RecipeId, 2);
        }
    }
}
