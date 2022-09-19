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

namespace BoardRoomSystem.Controllers
{
    [Authorize(Roles = "Admin, User")]
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
            var query = (from Event in dc.Events
                         join MeetingRoom in dc.MeetingRooms on Event.MeetingRoom.IdMeetR equals MeetingRoom.IdMeetR
                         select new
                         {
                             meetRId = MeetingRoom.IdMeetR,
                             meetRName = MeetingRoom.NameMeetR
                         }).ToList();

            var events = dc.Events.ToList();
            return Json(events, new System.Text.Json.JsonSerializerOptions { });

        }

        public JsonResult GetRooms()
        {

            var query2 = (from A in dc.Events
                          join B in dc.MeetingRooms on A.MeetingRoom.IdMeetR equals B.IdMeetR
                          join C in dc.Users on A.ApplicationUser.Id equals C.Id
                          select new {A.EventID, A.Subject, A.Start, A.End, A.Description, A.IsFullDay, A.ThemeColor, B.IdMeetR, B.NameMeetR, B.IdLocation, B.Location.NameLocation, C.Id}).ToList();

           

            return Json(query2, new System.Text.Json.JsonSerializerOptions { });
        }



        [HttpPost]
        public ActionResult SaveEvent(Event e)
        {
            //List<MeetingRoom> meetLst;
            //meetLst = (from d in dc.MeetingRooms
            //           select d).ToList();
            var status = false;
            List<Event> eventLst;
            eventLst = (from d in dc.Events
                        select d).ToList();

            

            var claimsIdentity = User.Identity as ClaimsIdentity;
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
                        if (e.EventID > 0)
                        {
                            if (userIdValue == e.UserId)
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
                                    v.ThemeColor = e.ThemeColor;
                                    v.IdLocation = e.IdLocation;
                                    v.IdMeetR = e.IdMeetR;
                                    v.UserId = e.UserId;


                                    dc.SaveChanges();

                                    status = true;


                                    return new JsonResult(new { status = status });
                                }
                                
                            }
                            //else
                            //{
                            //    ViewBag.Message = "Error";
                            //    status = true;
                            //    return new JsonResult(new { status = status });

                            //}


                        }
                        else
                        {
                            foreach (var item1 in eventLst)
                            {

                                if (e.Start >= item1.Start)
                                {
                                    if (e.End <= item1.End)
                                    {
                                        if (e.IdMeetR == item1.IdMeetR)
                                        {
                                            ViewBag.Message = "Lo sentimos, esta solicitud ya existe.";
                                            return View(e);

                                            //status = true;


                                            //return new JsonResult(new { status = status });
                                        }


                                    }
                                }

                                
                                

                            }
                            dc.Events.Add(e);

                           

                        }
                        
                        
                    }
                    

                   
                }

            }


            
            dc.SaveChanges();

            status = true;


            return new JsonResult ( new { status = status } );
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

        [HttpPost]
        public ActionResult DeleteEvent(int eventID, Event e)
        {
            List<Event> eventLst;
            eventLst = (from d in dc.Events
                        select d).ToList();
            var status = false;

            var claimsIdentity = User.Identity as ClaimsIdentity;
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
    }
}
