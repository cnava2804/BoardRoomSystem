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
using System.Web.Helpers;

namespace BoardRoomSystem.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {

        //private readonly BoardRoomSystemDBContext dBContext;
        private BoardRoomSystemDBContext db = new BoardRoomSystemDBContext();
        private readonly IDAL _dal;
        private readonly UserManager<ApplicationUser> _usermanager;
        private List<SelectListItem> _locItems = new List<SelectListItem>();


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
            return View(_dal.GetReservations());
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

            db = new BoardRoomSystemDBContext();
            ViewBag.Location = new SelectList(GetLocationList(), "Location_Id", "Location_Name");


            return View(new ReservationsViewModel(_dal.GetLocations(), _dal.GetAreasViewModel(), _dal.GetMeetingRooms()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reservations reservations, IFormCollection form)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _dal.CreateReservations(form);
                    TempData["Alert"] = "Exito! Has creado una nueva reservación para: " + reservations.Reservation_Subject;
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["Alert"] = "Ha ocurrido un error" + ex.Message;
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
            TempData["Alert"] = "Has Eliminado una reservación.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public JsonResult FilterLoc(int Id)
        {
            List<ElementJsonIntKey> lst = new List<ElementJsonIntKey>();
            using (BoardRoomSystemDBContext db = new BoardRoomSystemDBContext()) 
            {
                lst = (from d in db.MeetingRooms
                       where d.Location_Id == Id
                       select new ElementJsonIntKey
                       {
                           Value = d.MTGR_Id,
                           Text = d.MTGR_Name
                       }
                      ).ToList();
            }

            //var meetingRooms = db.MeetingRooms.ToList().Where(p => p.Location_Id == Id);
            return new JsonResult(lst);
        }

        public class ElementJsonIntKey 
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        //public JsonResult GetLocationList(int LocationId)
        //{
        //    using (db = new BoardRoomSystemDBContext())
        //    {
        //        var meetingR = db.MeetingRooms.Where(x => x.Location_Id == LocationId).ToList();
        //        return new JsonResult(meetingR);
        //    }
        //}

        public List<Location> GetLocationList()
        {

            db = new BoardRoomSystemDBContext();

            List<Location> locations = db.Locations.ToList();

            return locations;


        }

        public ActionResult GetMeetingRoomsList(int locId)
        {

            using (db = new BoardRoomSystemDBContext())
            {
                List<MeetingRooms> mrList = db.MeetingRooms.Where(x => x.Location_Id == locId).ToList();
                ViewBag.ProductOPtions = new SelectList(mrList, "MTGR_Id", "MTGR_Name");
                return PartialView("DisplayProduct");
            }

        }

    }
}
