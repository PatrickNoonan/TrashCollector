namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "zipCode", c => c.String());
            AddColumn("dbo.Customers", "weeklyPickupDay", c => c.String());
            AddColumn("dbo.Customers", "specialOneTimePickup", c => c.String());
            AddColumn("dbo.Employees", "zipCode", c => c.String());
            AddColumn("dbo.Employees", "routeStartingPoint", c => c.String());
            AddColumn("dbo.Employees", "routeEndingPoint", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "routeEndingPoint");
            DropColumn("dbo.Employees", "routeStartingPoint");
            DropColumn("dbo.Employees", "zipCode");
            DropColumn("dbo.Customers", "specialOneTimePickup");
            DropColumn("dbo.Customers", "weeklyPickupDay");
            DropColumn("dbo.Customers", "zipCode");
        }
    }
}
