namespace ProjectFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentityPrimaryKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.Recipe", "User_Id", "dbo.User");
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropPrimaryKey("dbo.Role");
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.UserLogin");
            AddColumn("dbo.Recipe", "User_Id", c => c.Int());
            AlterColumn("dbo.Recipe", "UserId", c => c.String());
            AlterColumn("dbo.Role", "RoleId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserRole", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserRole", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserClaim", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserLogin", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Role", "RoleId");
            AddPrimaryKey("dbo.UserRole", new[] { "UserId", "RoleId" });
            AddPrimaryKey("dbo.User", "UserId");
            AddPrimaryKey("dbo.UserLogin", new[] { "LoginProvider", "ProviderKey", "UserId" });
            CreateIndex("dbo.Recipe", "User_Id");
            CreateIndex("dbo.UserClaim", "UserId");
            CreateIndex("dbo.UserLogin", "UserId");
            CreateIndex("dbo.UserRole", "UserId");
            CreateIndex("dbo.UserRole", "RoleId");
            AddForeignKey("dbo.Recipe", "User_Id", "dbo.User", "UserId");
            AddForeignKey("dbo.UserRole", "RoleId", "dbo.Role", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.UserClaim", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserLogin", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserRole", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            DropColumn("dbo.Role", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Role", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Recipe", "User_Id", "dbo.User");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.Recipe", new[] { "User_Id" });
            DropPrimaryKey("dbo.UserLogin");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.Role");
            AlterColumn("dbo.UserLogin", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserClaim", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.User", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRole", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRole", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Role", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Recipe", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Recipe", "User_Id");
            AddPrimaryKey("dbo.UserLogin", new[] { "LoginProvider", "ProviderKey", "UserId" });
            AddPrimaryKey("dbo.User", "UserId");
            AddPrimaryKey("dbo.UserRole", new[] { "UserId", "RoleId" });
            AddPrimaryKey("dbo.Role", "RoleId");
            CreateIndex("dbo.UserLogin", "UserId");
            CreateIndex("dbo.UserClaim", "UserId");
            CreateIndex("dbo.UserRole", "RoleId");
            CreateIndex("dbo.UserRole", "UserId");
            AddForeignKey("dbo.Recipe", "User_Id", "dbo.User", "UserId");
            AddForeignKey("dbo.UserRole", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserLogin", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserClaim", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserRole", "RoleId", "dbo.Role", "RoleId", cascadeDelete: true);
        }
    }
}
