﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProjectFood.Domain.Entities;

namespace ProjectFood.Domain.Concrete
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<EfdbContext>
    {
        protected override void Seed(EfdbContext context)
        {

            var regions = new List<Region>
            {
                new Region { RegionId = 1, RegionName = "Indian"},
                new Region { RegionId = 2, RegionName = "American"}
            };
            regions.ForEach(r => context.Regions.Add(r));
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Fast Food"},
                new Category { CategoryId = 2, CategoryName = "Main Entree"}
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    RecipeId = 1,
                    RecipeName = "KungPao Chicken",
                    RecipeDescription = "Spicy chicken with peanuts, similar to what is served in Chinese restaurants. " +
                                        "It is easy to make, and you can be as sloppy with the measurements as you want. " +
                                        "They reduce to a nice, thick sauce. Substitute cashews for peanuts, or bamboo shoots" +
                                        " for the water chestnuts. You can't go wrong! Enjoy!",
                    CreateDateTime = DateTime.Now,
                    UpdateDatetime = DateTime.Now,
                    RegionId = 2,
                    CategoryId = 2,
                    ImageData = File.ReadAllBytes(@"E:\Visual Studio 2013\Projects\ProjectFood\ProjectFood.WebUI\App_Data\Images\kungpao.jpg"),
                    ImageMimeType = "image/jpeg"
                },
                new Recipe
                {
                    RecipeId = 2,
                    RecipeName = "Tomato Basil Salmon",
                    RecipeDescription = "This quick salmon dish is perfect for a weeknight dinner. Serve with a side of sauteed spinach" +
                                        " and a glass of pinot noir.",
                    CreateDateTime = DateTime.Now,
                    UpdateDatetime = DateTime.Now,
                    RegionId = 2,
                    CategoryId = 1,
                    ImageData = File.ReadAllBytes(@"E:\Visual Studio 2013\Projects\ProjectFood\ProjectFood.WebUI\App_Data\Images\tomato-basil-salmon.jpg"),
                    ImageMimeType = "image/jpeg"
                },
                new Recipe
                {
                    RecipeId = 3,
                    RecipeName = "Vegetarian Korma",
                    RecipeDescription = "This is an easy and exotic Indian dish. It's rich, creamy, mildly spiced, and extremely " +
                                        "flavorful. Serve with naan and rice.",
                    CreateDateTime = DateTime.Now,
                    UpdateDatetime = DateTime.Now,
                    RegionId = 1,
                    CategoryId = 2,
                    ImageData = File.ReadAllBytes(@"E:\Visual Studio 2013\Projects\ProjectFood\ProjectFood.WebUI\App_Data\Images\vegetarian-korma.jpg"),
                    ImageMimeType = "image/jpeg"
                },
                new Recipe
                {
                    RecipeId = 4,
                    RecipeName = "Blueberry Flax Pancakes",
                    RecipeDescription = "Fluffy pancakes with ground flax seed and blueberries for a healthier, fiber filled pancake.",
                    CreateDateTime = DateTime.Now,
                    UpdateDatetime = DateTime.Now,
                    RegionId = 2,
                    CategoryId = 1,
                    ImageData = File.ReadAllBytes(@"E:\Visual Studio 2013\Projects\ProjectFood\ProjectFood.WebUI\App_Data\Images\blueberry-flax-pancakes.jpg"),
                    ImageMimeType = "image/jpeg"
                }
            };
            recipes.ForEach(r => context.Recipes.Add(r));
            context.SaveChanges();
        }
    }
}