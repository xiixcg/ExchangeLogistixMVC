namespace ExchangeLogistixMVC.TrailerDBContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trailers", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trailers", "UserID");
        }
    }
}
