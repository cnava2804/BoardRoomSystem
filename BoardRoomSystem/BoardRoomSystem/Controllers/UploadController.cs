using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardRoomSystem.Models;
using BoardRoomSystem.Models.ViewModel;
using System.IO;
using System.Linq;
using BoardRoomSystem.Areas.Identity.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BoardRoomSystem.Controllers
{
    public class UploadController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHostingEnvironment environment;

        public UploadController(ApplicationDbContext dbContext, IHostingEnvironment environment)
        {
            this.dbContext = dbContext;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadImage(ImageCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var path = environment.WebRootPath;
                var filePath = "Content/Images/" + model.ImagePath.FileName;
                var fullPath = Path.Combine(path, filePath);
                UploadFile(model.ImagePath, fullPath);
                var data = new MeetingRoom()
                {
                    ImagePath = filePath
                };
                dbContext.Add(data);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }

        }

        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }


        public IActionResult Index()
        {
            var data = dbContext.MeetingRooms.ToList();
            return View(data);
        }
    }
}
