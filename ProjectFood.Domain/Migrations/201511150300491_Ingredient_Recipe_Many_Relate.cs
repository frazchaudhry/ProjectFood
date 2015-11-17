namespace ProjectFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ingredient_Recipe_Many_Relate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredient", "RecipeId", "dbo.Recipe");
            DropIndex("dbo.Ingredient", new[] { "RecipeId" });
            CreateTable(
                "dbo.IngredientRecipe",
                c => new
                    {
                        Ingredient_IngredientId = c.Int(nullable: false),
                        Recipe_RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ingredient_IngredientId, t.Recipe_RecipeId })
                .ForeignKey("dbo.Ingredient", t => t.Ingredient_IngredientId, cascadeDelete: true)
                .ForeignKey("dbo.Recipe", t => t.Recipe_RecipeId, cascadeDelete: true)
                .Index(t => t.Ingredient_IngredientId)
                .Index(t => t.Recipe_RecipeId);
            
            DropColumn("dbo.Ingredient", "RecipeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredient", "RecipeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.IngredientRecipe", "Recipe_RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.IngredientRecipe", "Ingredient_IngredientId", "dbo.Ingredient");
            DropIndex("dbo.IngredientRecipe", new[] { "Recipe_RecipeId" });
            DropIndex("dbo.IngredientRecipe", new[] { "Ingredient_IngredientId" });
            DropTable("dbo.IngredientRecipe");
            CreateIndex("dbo.Ingredient", "RecipeId");
            AddForeignKey("dbo.Ingredient", "RecipeId", "dbo.Recipe", "RecipeId", cascadeDelete: true);
        }
    }
}
