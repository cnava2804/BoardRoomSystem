using BoardRoomSystem.Data;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
           
            var events = dBContext.Reservations.ToList();
            return null;
          
        }

    }
}
