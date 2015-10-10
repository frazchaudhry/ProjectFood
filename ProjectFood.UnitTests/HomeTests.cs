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
    public class HomeTests
    {
        [TestMethod]
        public void Can_Send_Recipe_To_View()
        {
            // Arrange
            // Create the mock recipe repository
            Mock<IUnitOfWork> mock = new Mock<IUnitOfWork>();
            mock.Setup(r => r.RecipeRepository.Get(null, null, "")).Returns(new Recipe[]
            {
                new Recipe { RecipeId = 1, RecipeName = "Recipe1" },
                new Recipe { RecipeId = 2, RecipeName = "Recipe2" },
                new Recipe { RecipeId = 3, RecipeName = "Recipe3" },
                new Recipe { RecipeId = 4, RecipeName = "Recipe4" }
            });

            // Arrange - Create the controller
            HomeController target = new HomeController(mock.Object);

            // Act - get the model from the controller
            var result = ((IEnumerable<Recipe>) target.Index().Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 4);
            Assert.AreEqual(result[0].RecipeId, 1);
            Assert.AreEqual(result[0].RecipeName, "Recipe1");
            Assert.AreEqual(result[1].RecipeId, 2);
            Assert.AreEqual(result[1].RecipeName, "Recipe2");
            Assert.AreEqual(result[2].RecipeId, 3);
            Assert.AreEqual(result[2].RecipeName, "Recipe3");
            Assert.AreEqual(result[3].RecipeId, 4);
            Assert.AreEqual(result[3].RecipeName, "Recipe4");
        }
    }
}
