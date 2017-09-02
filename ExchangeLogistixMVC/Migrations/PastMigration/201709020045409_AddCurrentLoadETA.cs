namespace ExchangeLogistixMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentLoadETA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trailers", "CurrentLoadETA", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trailers", "CurrentLoadETA");
        }
    }
}
