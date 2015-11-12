namespace ProjectFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "AboutMe", c => c.String());
            AddColumn("dbo.User", "Address1", c => c.String());
            AddColumn("dbo.User", "Address2", c => c.String());
            AddColumn("dbo.User", "City", c => c.String());
            AddColumn("dbo.User", "State", c => c.String());
            AddColumn("dbo.User", "ZipCode", c => c.String());
            AddColumn("dbo.User", "Country", c => c.String());
            AddColumn("dbo.User", "ImageData", c => c.Binary());
            AddColumn("dbo.User", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ImageMimeType");
            DropColumn("dbo.User", "ImageData");
            DropColumn("dbo.User", "Country");
            DropColumn("dbo.User", "ZipCode");
            DropColumn("dbo.User", "State");
            DropColumn("dbo.User", "City");
            DropColumn("dbo.User", "Address2");
            DropColumn("dbo.User", "Address1");
            DropColumn("dbo.User", "AboutMe");
        }
    }
}
