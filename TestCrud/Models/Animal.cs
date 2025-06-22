using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TestCrud.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}