namespace ProjectFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipeUserRelate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipe", "User_Id", "dbo.User");
            DropIndex("dbo.Recipe", new[] { "User_Id" });
            DropColumn("dbo.Recipe", "UserId");
            RenameColumn(table: "dbo.Recipe", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Recipe", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Recipe", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Recipe", "UserId");
            AddForeignKey("dbo.Recipe", "UserId", "dbo.User", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipe", "UserId", "dbo.User");
            DropIndex("dbo.Recipe", new[] { "UserId" });
            AlterColumn("dbo.Recipe", "UserId", c => c.Int());
            AlterColumn("dbo.Recipe", "UserId", c => c.String());
            RenameColumn(table: "dbo.Recipe", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Recipe", "UserId", c => c.String());
            CreateIndex("dbo.Recipe", "User_Id");
            AddForeignKey("dbo.Recipe", "User_Id", "dbo.User", "UserId");
        }
    }
}
