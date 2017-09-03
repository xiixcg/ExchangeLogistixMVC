namespace ExchangeLogistixMVC.TrailerDBContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trailers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false),
                        ChasisSize = c.Int(nullable: false),
                        LoadSize = c.Int(nullable: false),
                        NextLoadLocation = c.String(nullable: false),
                        CurrentLoadDestination = c.String(nullable: false),
                        CurrentLoadETA = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trailers");
        }
    }
}
