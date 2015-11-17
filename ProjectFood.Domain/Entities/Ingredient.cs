using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectFood.Domain.Entities
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
