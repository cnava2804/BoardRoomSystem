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
using System.Collections;

namespace BoardRoomSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private BoardRoomSystemDBContext db = new BoardRoomSystemDBContext();
        private readonly ILogger<HomeController> _logger;
        private readonly IDAL _idal;
        private readonly UserManager<ApplicationUser> _usermanager;

        public HomeController(ILogger<HomeController> logger, IDAL idal, UserManager<ApplicationUser> usermanager)
        {
            _logger = logger;
            _idal = idal;
            _usermanager = usermanager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //ViewBag["Location"] = new SelectList(GetLocationList(), "Location_Id", "Location_Name");

            ViewBag.Location = new SelectList(GetLocationList(), "Location_Id", "Location_Name");
            ViewBag.MeetingRooms = new SelectList(GetMeetingRList(), "MTGR_Id", "MTGR_Name");
            ViewBag.AreasViewModel = new SelectList(GetAreasList(), "Area_Id", "Area_Name");

            ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(_idal.GetLocations());
            ViewData["Reservations"] = JSONListHelper.GetReservationListJSONString(_idal.GetReservations());

            return View(new ReservationsViewModel(_idal.GetLocations(), _idal.GetAreasViewModel(), _idal.GetMeetingRooms()));
        }

        [HttpPost]
        public ActionResult Index(IFormCollection form, [Bind("Reservation_Id,Reservation_Subject,Reservation_Recipient,Reservation_StartDate,Reservation_EndtDate,Reservation_NumbPeople,Reservation_Description,Reservation_Delegate,Location_Id,MTGR_Id,Area_Id")] BoardRoomSystem.Models.Reservations reservations)
        {
            //using (BoardRoomSystemDBContext entities = new BoardRoomSystemDBContext())
            //{
            //    _idal.CreateReservations(form);
            //    //entities.Reservations.Add(reservations);
            //    //entities.SaveChanges();
            //    int id = reservations.Reservation_Id;
            //}

            //ViewBag.Location = new SelectList(GetLocationList(), "Location_Id", "Location_Name");
            //ViewBag.MeetingRooms = new SelectList(GetMeetingRList(), "MTGR_Id", "MTGR_Name");
            //ViewBag.AreasViewModel = new SelectList(GetAreasList(), "Area_Id", "Area_Name");
            //return View();



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



            //if (ModelState.IsValid)
            //{
            //    db.Add(reservations);
            //    db.SaveChanges();
            ////////}

            //ViewBag.Location = new SelectList(GetLocationList(), "Location_Id", "Location_Name");
            //ViewBag.MeetingRooms = new SelectList(GetMeetingRList(), "MTGR_Id", "MTGR_Name");
            //ViewBag.AreasViewModel = new SelectList(GetAreasList(), "Area_Id", "Area_Name");

            //return View(nameof(Index));

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

        //[HttpPost]
        //public JsonResult SaveEvent(BoardRoomSystem.Models.Reservations e)
        //{
        //    var status = false;
        //    using (BoardRoomSystemDBContext dc = new BoardRoomSystemDBContext())
        //    {
        //        if (e.Reservation_Id > 0)
        //        {
        //            //Update the event
        //            var v = dc.Reservations.Where(a => a.Reservation_Id == e.Reservation_Id).FirstOrDefault();
        //            if (v != null)
        //            {
        //                v.Reservation_Subject = e.Reservation_Subject;
        //                v.Reservation_StartDate = e.Reservation_StartDate;
        //                v.Reservation_EndtDate = e.Reservation_EndtDate;
        //                v.Reservation_Description = e.Reservation_Description;
        //                //v.IsFullDay = e.IsFullDay;
        //                //v.ThemeColor = e.ThemeColor;
        //            }
        //        }
        //        else
        //        {
        //            dc.Reservations.Add(e);
        //        }

        //        dc.SaveChanges();
        //        status = true;

        //    }
        //    return new JsonResult(new { status = status });
        //}

        //[HttpPost]
        //public JsonResult DeleteEvent(int reservationID)
        //{
        //    var status = false;
        //    using (BoardRoomSystemDBContext dc = new BoardRoomSystemDBContext())
        //    {
        //        var v = dc.Reservations.Where(a => a.Reservation_Id == reservationID).FirstOrDefault();
        //        if (v != null)
        //        {
        //            dc.Reservations.Remove(v);
        //            dc.SaveChanges();
        //            status = true;
        //        }
        //    }
        //    return new JsonResult(new { status = status });

        //}

        //public IActionResult SaveReservation()
        //{
        //    db = new BoardRoomSystemDBContext();
        //    ViewBag.Location = new SelectList(GetLocationList(), "Location_Id", "Location_Name");


        //    return View(new ReservationsViewModel(_idal.GetLocations(), _idal.GetAreasViewModel(), _idal.GetMeetingRooms()));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult SaveReservation(Models.Reservations reservations, IFormCollection form)
        //{
        //    try
        //    {
        //        _idal.CreateReservations(form);
        //        return RedirectToAction("Index");

        //    }
        //    catch (Exception ex)
        //    {

        //        ViewData["Alert"] = "Ha ocurrido un error" + ex.Message;
        //        return View();
        //    }

        //}
        public List<Location> GetLocationList()
        {

            db = new BoardRoomSystemDBContext();

            List<Location> locations = db.Locations.ToList();

            return locations;


        }

        public List<MeetingRooms> GetMeetingRList()
        {

            db = new BoardRoomSystemDBContext();

            List<MeetingRooms> meetingRooms = db.MeetingRooms.ToList();

            return meetingRooms;


        }

        public List<AreasViewModel> GetAreasList()
        {

            db = new BoardRoomSystemDBContext();

            List<AreasViewModel> areasViewModels = db.AreasViewModels.ToList();

            return areasViewModels;


        }

    }
}
