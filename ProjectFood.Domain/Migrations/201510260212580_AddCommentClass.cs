namespace ProjectFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredient", "Recipe_RecipeId", "dbo.Recipe");
            DropIndex("dbo.Ingredient", new[] { "Recipe_RecipeId" });
            RenameColumn(table: "dbo.Ingredient", name: "Recipe_RecipeId", newName: "RecipeId");
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Recipe", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            AlterColumn("dbo.Ingredient", "RecipeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ingredient", "RecipeId");
            AddForeignKey("dbo.Ingredient", "RecipeId", "dbo.Recipe", "RecipeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredient", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.Comment", "RecipeId", "dbo.Recipe");
            DropIndex("dbo.Comment", new[] { "RecipeId" });
            DropIndex("dbo.Ingredient", new[] { "RecipeId" });
            AlterColumn("dbo.Ingredient", "RecipeId", c => c.Int());
            DropTable("dbo.Comment");
            RenameColumn(table: "dbo.Ingredient", name: "RecipeId", newName: "Recipe_RecipeId");
            CreateIndex("dbo.Ingredient", "Recipe_RecipeId");
            AddForeignKey("dbo.Ingredient", "Recipe_RecipeId", "dbo.Recipe", "RecipeId");
        }
    }
}
