namespace AzureAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ProfileKey = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Telephone = c.String()
                })
                .PrimaryKey(t => t.ProfileKey);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profile");
        }
    }
}
