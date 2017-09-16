namespace ExchangeLogistixMVC.ApplicationDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEnumForLocation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trailers", "NextLoadLocation", c => c.Int(nullable: false));
            AlterColumn("dbo.Trailers", "CurrentLoadDestination", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trailers", "CurrentLoadDestination", c => c.String(nullable: false));
            AlterColumn("dbo.Trailers", "NextLoadLocation", c => c.String(nullable: false));
        }
    }
}
