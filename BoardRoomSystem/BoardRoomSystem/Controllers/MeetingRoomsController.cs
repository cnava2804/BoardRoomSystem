using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models;
using BoardRoomSystem.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BoardRoomSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MeetingRoomsController : Controller
    {
        private readonly ApplicationDbContext dBContext;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHostingEnvironment environment;



        public MeetingRoomsController(ApplicationDbContext dBContext, IHostingEnvironment environment)
        {
            this.dBContext = dBContext;
            this.environment = environment;
        }
     
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = dBContext.MeetingRooms.Include(d => d.State).Include(l => l.Location);
            return View(await applicationDbContext.ToListAsync());
        }

        public JsonResult LocationJr()
        {
            var lct = dBContext.Locations.ToList();
            return new JsonResult(lct);
        }

        public JsonResult meetingRJr(int id)
        {
            var meetR = dBContext.MeetingRooms.Where(m => m.Location.IdLocation == id).ToList();
            return new JsonResult(meetR);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingR = await dBContext.MeetingRooms
                .Include(x => x.State)
                .Include(x => x.Location).FirstOrDefaultAsync(x => x.IdMeetR == id);

            if (meetingR == null)
            {
                return NotFound();
            }

            return View(meetingR);


        }

        //Crear por medio de Vista
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name");
            ViewData["IdLocation"] = new SelectList(dBContext.Locations, "IdLocation", "NameLocation");
            return View(new ImageCreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMeetR,NameMeetR,DescriptionMeetR,MaxNumbPeopleMeetR,ImagePath,State_Id,IdLocation")] ImageCreateModel model)
        {

            if (ModelState.IsValid)
            {
                var loc = model.IdLocation;
                var states = model.State_Id;
                var path = environment.WebRootPath;
                var filePath = "Content/Images/" + model.ImagePath.FileName;
                var fullPath = Path.Combine(path, filePath);
                UploadFile(model.ImagePath, fullPath);
                var data = new MeetingRoom()
                {
                    NameMeetR = model.NameMeetR,
                    DescriptionMeetR = model.DescriptionMeetR,
                    MaxNumbPeopleMeetR = model.MaxNumbPeopleMeetR,
                    ImagePath = filePath,
                    IdLocation = loc,
                    State_Id = states
                };

                dBContext.Add(data);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name", model.State_Id);
            ViewData["IdLocation"] = new SelectList(dBContext.Locations, "IdLocation", "NameLocation", model.State_Id);

            return View(model);
        }

        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
        //private string GetUniqueFileName(string fileName)
        //{
        //    fileName = Path.GetFileName(fileName);
        //    return Path.GetFileNameWithoutExtension(fileName)
        //              + "_"
        //              + Guid.NewGuid().ToString().Substring(0, 4)
        //              + Path.GetExtension(fileName);
        //}


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingRoom = await dBContext.MeetingRooms.FindAsync(id);

            if (meetingRoom == null)
            {
                return NotFound();
            }
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name", meetingRoom.State);
            ViewData["IdLocation"] = new SelectList(dBContext.Locations, "IdLocation", "NameLocation", meetingRoom.Location);

            return View(meetingRoom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMeetR,NameMeetR,DescriptionMeetR,MaxNumbPeopleMeetR,ImagePath,State_Id,IdLocation")] MeetingRoom meetingR)
        {
            if (id != meetingR.IdMeetR)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dBContext.Update(meetingR);
                    await dBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name", meetingR.State);
            ViewData["IdLocation"] = new SelectList(dBContext.Locations, "IdLocation", "NameLocation", meetingR.Location);

            return View(meetingR);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingR = await dBContext.MeetingRooms
               .Include(x => x.State)
               .Include(x => x.Location).FirstOrDefaultAsync(x => x.IdMeetR == id);

            if (meetingR == null)
            {
                return NotFound();
            }

            return View(meetingR);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var meetingR = await dBContext.MeetingRooms.FindAsync(id);
            dBContext.MeetingRooms.Remove(meetingR);
            await dBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}


