using BoardRoomSystem.Data;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Internal.Execution;
using System.Linq;

namespace BoardRoomSystem.Controllers
{
    public class HomeCalendarController : Controller
    {
        private readonly BoardRoomSystemDBContext dBContext;

        public HomeCalendarController(BoardRoomSystemDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            using (BoardRoomSystemDBContext dc = new BoardRoomSystemDBContext())
            {
                var events = dc.HomeCalendars.ToList();
                return new JsonResult(events, new Newtonsoft.Json.JsonSerializerSettings());
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(BoardRoomSystem.Models.HomeCalendar e)
        {
            var status = false;
            using (BoardRoomSystemDBContext dc = new BoardRoomSystemDBContext())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.HomeCalendars.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.HomeCalendars.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult(new { status = status });
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (BoardRoomSystemDBContext dc = new BoardRoomSystemDBContext())
            {
                var v = dc.HomeCalendars.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.HomeCalendars.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult(new { status = status });

        }
    }
}

