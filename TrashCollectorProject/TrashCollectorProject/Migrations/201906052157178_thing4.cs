namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "holdPickUpStart", c => c.String());
            AddColumn("dbo.Customers", "holdPickUpEnd", c => c.String());
            AddColumn("dbo.Customers", "bill", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "bill");
            DropColumn("dbo.Customers", "holdPickUpEnd");
            DropColumn("dbo.Customers", "holdPickUpStart");
        }
    }
}
