using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BoardRoomSystem.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using BoardRoomSystem.Models.ViewModels;
using Microsoft.AspNetCore.Http;


namespace BoardRoomSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private BoardRoomSystemDBContext db = new BoardRoomSystemDBContext();
        private readonly ILogger<HomeController> _logger;
        private readonly IDAL _idal;
        //private readonly IDAL _dal;
        private readonly UserManager<ApplicationUser> _usermanager;

        public HomeController(ILogger<HomeController> logger, IDAL idal, UserManager<ApplicationUser> usermanager)
        {
            _logger = logger;
            _idal = idal;
            _usermanager = usermanager;
        }

        public IActionResult Index()
        {
            ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(_idal.GetLocations());
            ViewData["Reservations"] = JSONListHelper.GetReservationListJSONString(_idal.GetReservations());

            return View();
        }

        [Authorize]
        public IActionResult MyCalendar()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(_idal.GetLocations());
            ViewData["Reservations"] = JSONListHelper.GetReservationListJSONString(_idal.GetMyReservations(userid));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ViewResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }


        public JsonResult GetEvents()
        {
            using (BoardRoomSystemDBContext dc = new BoardRoomSystemDBContext())
            {
                var events = dc.Reservations.ToList();
                return new JsonResult(events, new Newtonsoft.Json.JsonSerializerSettings());
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(BoardRoomSystem.Models.Reservations e)
        {
            var status = false;
            using (BoardRoomSystemDBContext dc = new BoardRoomSystemDBContext())
            {
                if (e.Reservation_Id > 0)
                {
                    //Update the event
                    var v = dc.Reservations.Where(a => a.Reservation_Id == e.Reservation_Id).FirstOrDefault();
                    if (v != null)
                    {
                        v.Reservation_Subject = e.Reservation_Subject;
                        v.Reservation_StartDate = e.Reservation_StartDate;
                        v.Reservation_EndtDate = e.Reservation_EndtDate;
                        v.Reservation_Description = e.Reservation_Description;
                        //v.IsFullDay = e.IsFullDay;
                        //v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Reservations.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult(new { status = status });
        }

        [HttpPost]
        public JsonResult DeleteEvent(int reservationID)
        {
            var status = false;
            using (BoardRoomSystemDBContext dc = new BoardRoomSystemDBContext())
            {
                var v = dc.Reservations.Where(a => a.Reservation_Id == reservationID).FirstOrDefault();
                if (v != null)
                {
                    dc.Reservations.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult(new { status = status });

        }

        public IActionResult Create()
        {
            //List<SelectListItem> lst = new List<SelectListItem>();

            //using (Data.BoardRoomSystemDBContext dB = new BoardRoomSystemDBContext())
            //{
            //    lst = (from d in dB.Locations
            //           select new SelectListItem
            //           {
            //               Value = d.Location_Id.ToString(),
            //               Text = d.Location_Name
            //           }).ToList();
            //}

            db = new BoardRoomSystemDBContext();
            ViewBag.Location = new SelectList(GetLocationList(), "Location_Id", "Location_Name");

            //using (db = new BoardRoomSystemDBContext())
            //{
            //    //// CARGAMOS EL DropDownList DE REGIONES
            //    //var loc = db.Locations.ToList();
            //    //_locItems = new List<SelectListItem>();
            //    //foreach (var item in loc)
            //    //{
            //    //    _locItems.Add(new SelectListItem
            //    //    {
            //    //        Text = item.Location_Name,
            //    //        Value = item.Location_Id.ToString()
            //    //    });
            //    //}
            //    //ViewBag.locItems = _locItems;
            //}

            return View(new ReservationsViewModel(_idal.GetLocations(), _idal.GetAreasViewModel(), _idal.GetMeetingRooms()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.Reservations reservations, IFormCollection form)
        {
            try
            {


                _idal.CreateReservations(form);
                TempData["Alert"] = "Exito! Has creado una nueva reservación para: " + reservations.Reservation_Subject;
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {

                ViewData["Alert"] = "Ha ocurrido un error" + ex.Message;
                return View(reservations);
            }

        }
        public List<Location> GetLocationList()
        {

            db = new BoardRoomSystemDBContext();

            List<Location> locations = db.Locations.ToList();

            return locations;


        }

    }
}
