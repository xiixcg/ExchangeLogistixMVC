namespace ExchangeLogistixMVC.ApplicationDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedChasisSizeEnumAndCreatedDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trailers", "CreatedDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trailers", "CreatedDateTime");
        }
    }
}
