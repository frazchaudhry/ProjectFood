namespace ProjectFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIngredients : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IngredientRecipe", "Ingredient_IngredientId", "dbo.Ingredient");
            DropForeignKey("dbo.IngredientRecipe", "Recipe_RecipeId", "dbo.Recipe");
            DropIndex("dbo.IngredientRecipe", new[] { "Ingredient_IngredientId" });
            DropIndex("dbo.IngredientRecipe", new[] { "Recipe_RecipeId" });
            AddColumn("dbo.Recipe", "Ingredients", c => c.String());
            DropTable("dbo.Ingredient");
            DropTable("dbo.IngredientRecipe");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IngredientRecipe",
                c => new
                    {
                        Ingredient_IngredientId = c.Int(nullable: false),
                        Recipe_RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ingredient_IngredientId, t.Recipe_RecipeId });
            
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        IngredientId = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(),
                    })
                .PrimaryKey(t => t.IngredientId);
            
            DropColumn("dbo.Recipe", "Ingredients");
            CreateIndex("dbo.IngredientRecipe", "Recipe_RecipeId");
            CreateIndex("dbo.IngredientRecipe", "Ingredient_IngredientId");
            AddForeignKey("dbo.IngredientRecipe", "Recipe_RecipeId", "dbo.Recipe", "RecipeId", cascadeDelete: true);
            AddForeignKey("dbo.IngredientRecipe", "Ingredient_IngredientId", "dbo.Ingredient", "IngredientId", cascadeDelete: true);
        }
    }
}
