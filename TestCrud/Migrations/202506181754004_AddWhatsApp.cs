namespace TestCrud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWhatsApp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WhatsAppMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        TemplateName = c.String(),
                        LanguageCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.WhatsAppMessages");
        }
    }
}
