namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "city", c => c.String());
            AddColumn("dbo.Employees", "state", c => c.String());
            DropColumn("dbo.Employees", "emailAddress");
            DropColumn("dbo.Employees", "address");
            DropColumn("dbo.Employees", "routeStartingPoint");
            DropColumn("dbo.Employees", "routeEndingPoint");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "routeEndingPoint", c => c.String());
            AddColumn("dbo.Employees", "routeStartingPoint", c => c.String());
            AddColumn("dbo.Employees", "address", c => c.String());
            AddColumn("dbo.Employees", "emailAddress", c => c.String());
            DropColumn("dbo.Employees", "state");
            DropColumn("dbo.Employees", "city");
        }
    }
}
