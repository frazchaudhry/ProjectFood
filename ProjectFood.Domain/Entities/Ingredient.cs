﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectFood.Domain.Entities
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
