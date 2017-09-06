namespace ExchangeLogistixMVC.TrailerDBContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PossibleChangeInPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trailers", "UserID", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trailers", "UserID", c => c.String());
        }
    }
}
