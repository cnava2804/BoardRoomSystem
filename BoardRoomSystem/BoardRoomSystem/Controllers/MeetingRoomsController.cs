using BoardRoomSystem.Data;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
namespace BoardRoomSystem.Controllers
{
    [Authorize]
    public class MeetingRoomsController : Controller
    {
        private readonly BoardRoomSystemDBContext dBContext;

        public MeetingRoomsController(BoardRoomSystemDBContext dBContext)
        {
            this.dBContext = dBContext;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MTGR_Id,MTGR_Name,MTGR_Description,MTGR_MaxNumbPeople,MTGR_Image,MTGR_NumbRoom,State_Id,Location_Id")] MeetingRooms meetingRoom)
        {

            if (ModelState.IsValid)
            {

                dBContext.Add(meetingRoom);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name", meetingRoom.States);
            ViewData["Location_Id"] = new SelectList(dBContext.Locations, "Location_Id", "Location_Name", meetingRoom.Location);

            return View(meetingRoom);
        }


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


