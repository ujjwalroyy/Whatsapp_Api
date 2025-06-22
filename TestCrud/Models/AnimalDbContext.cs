
using System.Data.Entity;


namespace TestCrud.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<WhatsAppMessage> Messages { get; set; }



    }
}