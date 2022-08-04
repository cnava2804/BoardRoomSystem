using Microsoft.AspNetCore.Mvc;
using BoardRoomSystem.Data;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BoardRoomSystem.Controllers
{
    public class ReservationsController : Controller
    {

        private readonly BoardRoomSystemDBContext dBContext;

        public ReservationsController(BoardRoomSystemDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = dBContext.Reservations.Include(d => d.Reservation_Id);
            return View(await dBContext.Reservations.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await dBContext.Reservations.FirstOrDefaultAsync(r => r.Reservation_Id == id);

            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);


        }

        //Crear por medio de Vista

        public IActionResult Create()
        {
            ViewData["State_Id"] = new SelectList(dBContext.States, "State_Id", "state_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservations reservations)
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

                dBContext.Add(reservations);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservations);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await dBContext.Reservations.FindAsync(id);

            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservations reservations)
        {
            if (id != reservations.Reservation_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dBContext.Update(reservations);
                    await dBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(reservations);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = await dBContext.Reservations.FirstOrDefaultAsync(r => r.Reservation_Id == id);

            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var reservations = await dBContext.Reservations.FindAsync(id);
            dBContext.Reservations.Remove(reservations);
            await dBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
