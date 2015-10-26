namespace ProjectFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        IngredientId = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(),
                        Recipe_RecipeId = c.Int(),
                    })
                .PrimaryKey(t => t.IngredientId)
                .ForeignKey("dbo.Recipe", t => t.Recipe_RecipeId)
                .Index(t => t.Recipe_RecipeId);
            
            CreateTable(
                "dbo.Recipe",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        RecipeName = c.String(),
                        RecipeDescription = c.String(),
                        Directions = c.String(),
                        IsVegetarian = c.Boolean(nullable: false),
                        RegionId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        UserId = c.Int(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        UpdateDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Region", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        RegionName = c.String(),
                    })
                .PrimaryKey(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipe", "RegionId", "dbo.Region");
            DropForeignKey("dbo.Ingredient", "Recipe_RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.Recipe", "CategoryId", "dbo.Category");
            DropIndex("dbo.Recipe", new[] { "CategoryId" });
            DropIndex("dbo.Recipe", new[] { "RegionId" });
            DropIndex("dbo.Ingredient", new[] { "Recipe_RecipeId" });
            DropTable("dbo.Region");
            DropTable("dbo.Recipe");
            DropTable("dbo.Ingredient");
            DropTable("dbo.Category");
        }
    }
}
