namespace AzureAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fieldAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "ImageUrl");
        }
    }
}
