using BoardRoomSystem.Models;
using BoardRoomSystem.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using BoardRoomSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BoardRoomSystem.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dc;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dc)
        {
            _logger = logger;
            this.dc = dc;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {

            var query2 = (from A in dc.Events
                          join B in dc.MeetingRooms on A.MeetingRoom.IdMeetR equals B.IdMeetR
                          join C in dc.Users on A.ApplicationUser.Id equals C.Id
                          join D in dc.AreasViewModels on A.AreasViewModel.IdArea equals D.IdArea
                          select new {A.EventID, A.Subject, A.Start, A.End, A.Description, A.IsFullDay, A.NumOfPeople, B.IdMeetR, B.NameMeetR, B.ThemeColorMeetR, B.IdLocation, B.Location.NameLocation, C.Id, D.IdArea, D.NameArea}).ToList();

           

            return Json(query2, new System.Text.Json.JsonSerializerOptions { });
        }



        [HttpPost]
        public ActionResult SaveEvent(Event e)
        {
            var status = false;
            List<Event> eventLst;
            eventLst = (from d in dc.Events
                        select d).ToList();
            List<MeetingRoom> meetRLst;
            meetRLst = (from d in dc.MeetingRooms
                        select d).ToList();

            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (this.User.IsInRole("User") || this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin"))
            {
                if (claimsIdentity != null)
                {
                    // the principal identity is a claims identity.
                    // now we need to find the NameIdentifier claim
                    var userIdClaim = claimsIdentity.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                    
                    
                    if (userIdClaim != null)
                    {
                        var userIdValue = userIdClaim.Value;

                        foreach (var item2 in meetRLst)
                        {
                            if (e.NumOfPeople > item2.MaxNumbPeopleMeetR && e.IdMeetR == item2.IdMeetR)
                            {
                                ViewBag.Message = "Lo sentimos, el número de personas es demaciado alto para la sala.";
                                
                                return View(e);
                            }

                            if (e.NumOfPeople < item2.MinNumbPeopleMeetR && e.IdMeetR == item2.IdMeetR)
                            {
                                ViewBag.Message = "Lo sentimos, el número de personas es demaciado bajo para la sala.";
                                return View(e);
                            }
                        }

                        foreach (var item1 in eventLst)
                        {

                            if (e.Start >= item1.Start)
                            {
                                if (e.End <= item1.End || item1.IsFullDay)
                                {
                                    if (e.IdMeetR == item1.IdMeetR)
                                    {
                                        ViewBag.Message = "Lo sentimos, ya existe una reservación en esa fecha y sala.";
                                        //return View(e);

                                        status = true;


                                        return new JsonResult(e, new { status = status });
                                    }


                                }
                            }




                        }



                    }

                }
            }

            

            dc.Events.Add(e);

            dc.SaveChanges();

            status = true;


            return new JsonResult ( new { status = status } );

            
        }

        [HttpPost]
        public ActionResult SaveEventEd(Event e)
        {
            var status = false;
            List<Event> eventLst;
            eventLst = (from d in dc.Events
                        select d).ToList();
            List<MeetingRoom> meetRLst;
            meetRLst = (from d in dc.MeetingRooms
                        select d).ToList();

            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (this.User.IsInRole("User"))
            {
                if (claimsIdentity != null)
                {
                    // the principal identity is a claims identity.
                    // now we need to find the NameIdentifier claim
                    var userIdClaim = claimsIdentity.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                    if (userIdClaim != null)
                    {
                        var userIdValue = userIdClaim.Value;

                        if (userIdValue == e.UserId)
                        {
                            foreach (var item2 in meetRLst)
                            {
                                if (e.NumOfPeople > item2.MaxNumbPeopleMeetR && e.IdMeetR == item2.IdMeetR)
                                {
                                    ViewBag.Message = "Lo sentimos, el número de personas es demaciado alto para la sala.";
                                    return View(e);
                                }

                                if (e.NumOfPeople < item2.MinNumbPeopleMeetR && e.IdMeetR == item2.IdMeetR)
                                {
                                    ViewBag.Message = "Lo sentimos, el número de personas es demaciado bajo para la sala.";
                                    return View(e);
                                }
                            }

                            foreach (var item1 in eventLst)
                            {
                                if (e.Start >= item1.Start)
                                {
                                    if (e.End <= item1.End || item1.IsFullDay)
                                    {
                                        if (e.IdMeetR == item1.IdMeetR)
                                        {
                                            if (e.EventID == item1.EventID)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                ViewBag.Message = "Lo sentimos, ya existe una reservación en esa fecha y sala.";
                                                //return View(e);

                                                status = true;


                                                return new JsonResult(new { status = status }, e);
                                            }
                                        }


                                    }
                                }

                            }

                            //Update the event
                            var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                            if (v != null)
                            {
                                v.Subject = e.Subject;
                                v.Start = e.Start;
                                v.End = e.End;
                                v.Description = e.Description;
                                v.IsFullDay = e.IsFullDay;
                                v.NumOfPeople = e.NumOfPeople;
                                v.IdLocation = e.IdLocation;
                                v.IdMeetR = e.IdMeetR;
                                v.UserId = e.UserId;
                                v.IdArea = e.IdArea;


                                dc.SaveChanges();

                                status = true;


                                return new JsonResult(new { status = status });

                            }


                        }
                        else
                        {
                            ViewBag.Message = "Lo sentimos, usted no puede editar reservaciones de otros usuarios.";
                            return View(e);
                        }



                    }

                }
            }

            if (this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin"))
            {
                foreach (var item2 in meetRLst)
                {
                    if (e.NumOfPeople > item2.MaxNumbPeopleMeetR && e.IdMeetR == item2.IdMeetR)
                    {
                        ViewBag.Message = "Lo sentimos, el número de personas es demaciado alto para la sala.";
                        return View(e);
                    }

                    if (e.NumOfPeople < item2.MinNumbPeopleMeetR && e.IdMeetR == item2.IdMeetR)
                    {
                        ViewBag.Message = "Lo sentimos, el número de personas es demaciado bajo para la sala.";
                        return View(e);
                    }
                }

                foreach (var item1 in eventLst)
                {

                    if (e.Start >= item1.Start)
                    {
                        if (e.End <= item1.End || item1.IsFullDay)
                        {
                            if (e.IdMeetR == item1.IdMeetR)
                            {
                                if (e.EventID == item1.EventID)
                                {
                                    continue;
                                }
                                else
                                {
                                    ViewBag.Message = "Lo sentimos, ya existe una reservación en esa fecha y sala.";
                                    //return View(e);

                                    status = true;


                                    return new JsonResult(new { status = status }, e);
                                }

                            }


                        }
                    }



                }


                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.NumOfPeople = e.NumOfPeople;
                        v.IdLocation = e.IdLocation;
                        v.IdMeetR = e.IdMeetR;
                        v.UserId = e.UserId;
                        v.IdArea = e.IdArea;


                        dc.SaveChanges();

                        status = true;


                        return new JsonResult(new { status = status });
                    }


                }

            }


            dc.SaveChanges();

            status = true;


            return new JsonResult(new { status = status });
        }

        public JsonResult txtLocation()
        {
            var locs = dc.Locations.ToList();
            return new JsonResult(locs);
        }

        public JsonResult txtRoom(int id)
        {
            var rooms = dc.MeetingRooms.Where(m => m.Location.IdLocation == id).ToList();
            return new JsonResult(rooms);
        }

        public JsonResult txtLocationEd()
        {
            var locs = dc.Locations.ToList();
            return new JsonResult(locs);
        }

        public JsonResult txtRoomEd(int id)
        {
            var rooms = dc.MeetingRooms.Where(m => m.Location.IdLocation == id).ToList();
            return new JsonResult(rooms);
        }

        public JsonResult loc_selector()
        {
            var locs = dc.Locations.ToList();
            return new JsonResult(locs);
        }

        public JsonResult room_selector(int id)
        {
            var rooms = dc.MeetingRooms.Where(m => m.Location.IdLocation == id).ToList();
            return new JsonResult(rooms);
        }
        public JsonResult txtArea(int id)
        {
            var areas = dc.AreasViewModels.ToList();
            return new JsonResult(areas);
        }

        [HttpPost]
        public ActionResult DeleteEvent(int eventID, Event e)
        {
            List<Event> eventLst;
            eventLst = (from d in dc.Events
                        select d).ToList();
            var status = false;
            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (this.User.IsInRole("User"))
            {
                if (claimsIdentity != null)
                {
                    // the principal identity is a claims identity.
                    // now we need to find the NameIdentifier claim
                    var userIdClaim = claimsIdentity.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                    if (userIdClaim != null)
                    {
                        var userIdValue = userIdClaim.Value;

                        foreach (var itemss in eventLst)
                        {
                            if (e.EventID == itemss.EventID && itemss.UserId == userIdValue)
                            {

                                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                                if (v != null)
                                {
                                    dc.Events.Remove(v);
                                    dc.SaveChanges();
                                    status = true;
                                }
                            }
                        }
                        
                    }
                }
            }

            if (this.User.IsInRole("Admin") || this.User.IsInRole("SuperAdmin"))
            {
                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            


            return new JsonResult ( new { status = status } );
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AgendaView()
        {
            return View();
        }
    }
}
