using BoardRoomSystem.Data;
using BoardRoomSystem.Models;
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
    public class MeetingRoomsController : Controller
    {
        private readonly BoardRoomSystemDBContext dBContext;

        public MeetingRoomsController(BoardRoomSystemDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = dBContext.MeetingRooms.Include(d => d.States);
            return View(await dBContext.MeetingRooms.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingR = await dBContext.MeetingRooms.FirstOrDefaultAsync(m => m.MTGR_Id == id);

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingRooms meetingRoom)
        {

            if (ModelState.IsValid)
            {
                //try
                //{
                //    HttpPostedFileBase file = Request.Files["ImageData"];
                //    byte[] img = ConvertToBytes(file);
                //}
                //catch (Exception)
                //{

                //    throw;
                //}

                dBContext.Add(meetingRoom);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meetingRoom);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingR = await dBContext.MeetingRooms.FindAsync(id);

            if (meetingR == null)
            {
                return NotFound();
            }

            return View(meetingR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MeetingRooms meetingR)
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

            return View(meetingR);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingR = await dBContext.MeetingRooms.FirstOrDefaultAsync(m => m.MTGR_Id == id);

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


