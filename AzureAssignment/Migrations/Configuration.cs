namespace AzureAssignment.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AzureAssignment.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<AzureAssignment.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AzureAssignment.Models.ApplicationDbContext";
        }

        protected override void Seed(AzureAssignment.Models.ApplicationDbContext context)
        {      

        }
    }
}
