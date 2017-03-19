namespace AzureAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profile", "ImageUrl", c => c.String());
        }
    }
}
