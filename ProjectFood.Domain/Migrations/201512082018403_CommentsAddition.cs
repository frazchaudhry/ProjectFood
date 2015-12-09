namespace ProjectFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comment", "UserId");
            AddForeignKey("dbo.Comment", "UserId", "dbo.User", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropColumn("dbo.Comment", "UserId");
        }
    }
}
