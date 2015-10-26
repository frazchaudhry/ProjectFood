using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Win32;

namespace ProjectFood.Domain.Entities
{
    public class Recipe
    {
        [HiddenInput(DisplayValue = false)]
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        [DataType(DataType.MultilineText)]
        public string RecipeDescription { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
        [DataType(DataType.MultilineText)]
        public string Directions { get; set; }
        public bool IsVegetarian { get; set; }
        public int RegionId { get; set; }
        public int CategoryId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public byte[] ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreateDateTime { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime UpdateDatetime { get; set; }
        public virtual Region Region { get; set; }
        public virtual Category Category { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
