namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "specialPickupConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "specialPickupConfirmed", c => c.Double(nullable: false));
        }
    }
}
