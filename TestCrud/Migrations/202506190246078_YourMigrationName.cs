namespace TestCrud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YourMigrationName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WhatsAppMessages", "MediaId");
            DropColumn("dbo.WhatsAppMessages", "MediaType");
            DropColumn("dbo.WhatsAppMessages", "Caption");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WhatsAppMessages", "Caption", c => c.String());
            AddColumn("dbo.WhatsAppMessages", "MediaType", c => c.String());
            AddColumn("dbo.WhatsAppMessages", "MediaId", c => c.String());
        }
    }
}
