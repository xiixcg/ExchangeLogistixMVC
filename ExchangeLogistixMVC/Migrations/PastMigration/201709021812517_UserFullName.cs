namespace ExchangeLogistixMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFullName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trailers", "FirstName");
            DropColumn("dbo.Trailers", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trailers", "LastName", c => c.String(nullable: false));
            AddColumn("dbo.Trailers", "FirstName", c => c.String(nullable: false));
        }
    }
}
