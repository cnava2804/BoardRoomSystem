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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models.ViewModels;
using BoardRoomSystem.Controllers.ActionFilters;

namespace BoardRoomSystem.Controllers
{
    public class ReservationsController : Controller
    {

        //private readonly BoardRoomSystemDBContext dBContext;
        private readonly IDAL _dal;

        public ReservationsController(IDAL dal)
        {
            _dal = dal;
        }


        //private readonly UserManager<ApplicationUser> _usermanager;

        //public ReservationsController(IDAL dal, UserManager<ApplicationUser> usermanager)
        //{
        //    _dal = dal;
        //    _usermanager = usermanager;
        //}

        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View (_dal.GetReservations());

            //var applicationDbContext = dBContext.Reservations.Include(d => d.Reservation_Id);
            //return View(await dBContext.Reservations.ToListAsync());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = _dal.GetReservations((int)id);

            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);


        }

        //Crear por medio de Vista

        public IActionResult Create()
        {
            return View(new ReservationsViewModel(_dal.GetLocations()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reservations reservations, IFormCollection form)
        {
            try
            {
                _dal.CreateReservations(form);
                TempData["Alert"] = "Exito! Has creado una nueva reservación para: " + reservations.Reservation_Subject;
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                ViewData["Alert"] = "Ha ocurrido un error" +  ex.Message;
                return View(reservations);
            }

        }

        // GET: Event/Edit/5
        //[UserAccessOnly]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @reservations = _dal.GetReservations((int)id);
            if (@reservations == null)
            {
                return NotFound();
            }
            var reserv = new ReservationsViewModel(@reservations, _dal.GetLocations());
            return View(reserv);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, IFormCollection form)
        {
            try
            {
                _dal.UpdateReservations(form);
                TempData["Alert"] = "Success! You modified an event for: " + form["Reservations.Reservation_Subject"];
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "An error occurred: " + ex.Message;
                var reserv = new ReservationsViewModel(_dal.GetReservations(id), _dal.GetLocations());
                return View(reserv);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservations = _dal.GetReservations((int)id);

            if (reservations == null)
            {
                return NotFound();
            }

            return View(reservations);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _dal.DeleteReservations((int)id);
            TempData["Alert"] = "Has Eliminado una reservación." ;
            return RedirectToAction(nameof(Index));
        }

    }
}
