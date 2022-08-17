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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models.ViewModels;
using BoardRoomSystem.Controllers.ActionFilters;

namespace BoardRoomSystem.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {

        //private readonly BoardRoomSystemDBContext dBContext;
        private BoardRoomSystemDBContext db = new BoardRoomSystemDBContext();
        private readonly IDAL _dal;
        private readonly UserManager<ApplicationUser> _usermanager;

        //public ReservationsController(IDAL dal)
        //{
        //    _dal = dal;
        //}

        public ReservationsController(IDAL dal, UserManager<ApplicationUser> usermanager)
        {
            _dal = dal;
            _usermanager = usermanager;
        }

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
            return View(new ReservationsViewModel(_dal.GetLocations(), _dal.GetAreasViewModel(), _dal.GetMeetingRooms()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reservations reservations, IFormCollection form)
        {
            try
            {
                //var model = new Reservations();
                //var modeloExistente = db.Reservations.FirstOrDefault(m => m.Reservation_StartDate == model.Reservation_StartDate && m.Reservation_EndtDate == model.Reservation_EndtDate.Date);

                //if (modeloExistente == null)
                //{
                   
                //}
                //else
                //{
                //    ViewBag.sms = "mostrar un mensaje diciendo que el registro ya existe.";
                //}

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
        [UserAccessOnly]
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
            var reserv = new ReservationsViewModel(@reservations, _dal.GetLocations(), _dal.GetAreasViewModel(), _dal.GetMeetingRooms());
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
                var reserv = new ReservationsViewModel(_dal.GetReservations(id), _dal.GetLocations(), _dal.GetAreasViewModel(), _dal.GetMeetingRooms());
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
