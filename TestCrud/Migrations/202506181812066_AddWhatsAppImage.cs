namespace TestCrud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWhatsAppImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WhatsAppMessages", "MediaId", c => c.String());
            AddColumn("dbo.WhatsAppMessages", "MediaType", c => c.String());
            AddColumn("dbo.WhatsAppMessages", "Caption", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WhatsAppMessages", "Caption");
            DropColumn("dbo.WhatsAppMessages", "MediaType");
            DropColumn("dbo.WhatsAppMessages", "MediaId");
        }
    }
}
