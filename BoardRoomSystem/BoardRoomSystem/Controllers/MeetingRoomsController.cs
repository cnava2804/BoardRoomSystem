using BoardRoomSystem.Data;
using BoardRoomSystem.Models;
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

namespace BoardRoomSystem.Controllers
{
    [Authorize]
    public class MeetingRoomsController : Controller
    {
        private readonly BoardRoomSystemDBContext dBContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

       

        public MeetingRoomsController(BoardRoomSystemDBContext dBContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dBContext = dBContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = dBContext.MeetingRooms.Include(d => d.States).Include(l => l.Location);
            return View(await dBContext.MeetingRooms.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingR = await dBContext.MeetingRooms
                .Include(x => x.States)
                .Include(x => x.Location).FirstOrDefaultAsync(x => x.MTGR_Id == id);

            if (meetingR == null)
            {
                return NotFound();
            }

            return View(meetingR);


        }

        //Crear por medio de Vista

        public IActionResult Create()
        {
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name");
            ViewData["Location_Id"] = new SelectList(dBContext.Locations, "Location_Id", "Location_Name");
            return View(new MeetingRooms());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MTGR_Id,MTGR_Name,MTGR_Description,MTGR_MaxNumbPeople,MTGR_Image,MTGR_ImageFile,MTGR_NumbRoom,State_Id,Location_Id")] MeetingRooms meetingRoom, ImagesModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.CoverPhoto != null)
                {
                    string folder = "images/cover";
                    folder += model.CoverPhoto.FileName+Guid.NewGuid().ToString();
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                }


                dBContext.Add(meetingRoom);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name", meetingRoom.States);
            ViewData["Location_Id"] = new SelectList(dBContext.Locations, "Location_Id", "Location_Name", meetingRoom.Location);

            return View(meetingRoom);
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
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name", meetingRoom.States);
            ViewData["Location_Id"] = new SelectList(dBContext.Locations, "Location_Id", "Location_Name", meetingRoom.Location);

            return View(meetingRoom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MTGR_Id,MTGR_Name,MTGR_Description,MTGR_MaxNumbPeople,MTGR_Image,MTGR_NumbRoom,State_Id,Location_Id")] MeetingRooms meetingR)
        {
            if (id != meetingR.MTGR_Id)
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
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name", meetingR.States);
            ViewData["Location_Id"] = new SelectList(dBContext.Locations, "Location_Id", "Location_Name", meetingR.Location);

            return View(meetingR);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingR = await dBContext.MeetingRooms
               .Include(x => x.States)
               .Include(x => x.Location).FirstOrDefaultAsync(x => x.MTGR_Id == id);

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


