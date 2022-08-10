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

namespace BoardRoomSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDAL _idal;
        private readonly UserManager<ApplicationUser> _usermanager;

        public HomeController(ILogger<HomeController> logger, IDAL idal, UserManager<ApplicationUser> usermanager)
        {
            _logger = logger;
            _idal = idal;
            _usermanager = usermanager;
        }

        public IActionResult Index()
        {
            //ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(_idal.GetLocations());
            //ViewData["Reservations"] = JSONListHelper.GetEventListJSONString(_idal.GetReservations());

            return View();
        }

        [Authorize]
        public IActionResult MyCalendar()
        {
            //var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(_idal.GetLocations());
            //ViewData["Reservations"] = JSONListHelper.GetEventListJSONString(_idal.GetMyReservations(userid));
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
    }
}
