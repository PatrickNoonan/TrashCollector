namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Bill", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Bill");
        }
    }
}
