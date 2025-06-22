using System.IO;
using System.Linq;
using System.Web.Mvc;
using TestCrud.Models;

namespace TestCrud.Controllers
{
    public class AnimalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() => View();

        [HttpGet]
        public JsonResult GetAnimals()
        {
            var data = db.Animals.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddAnimal(Animal animal)
        {
            if (animal.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(animal.ImageFile.FileName);
                string extension = Path.GetExtension(animal.ImageFile.FileName);
                fileName += extension;
                string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                animal.ImageFile.SaveAs(path);
                animal.ImagePath = "/Images/" + fileName;
            }

            db.Animals.Add(animal);
            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult UpdateAnimal(Animal animal)
        {
            var existing = db.Animals.Find(animal.Id);
            if (existing == null) return Json(new { success = false });

            existing.Name = animal.Name;
            existing.Category = animal.Category;
            existing.Description = animal.Description;

            if (animal.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(animal.ImageFile.FileName);
                string extension = Path.GetExtension(animal.ImageFile.FileName);
                fileName += extension;
                string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                animal.ImageFile.SaveAs(path);
                existing.ImagePath = "/Images/" + fileName;
            }

            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult DeleteAnimal(int id)
        {
            var animal = db.Animals.Find(id);
            if (animal == null) return Json(new { success = false });

            db.Animals.Remove(animal);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}