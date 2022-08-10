using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models;

namespace BoardRoomSystem.Controllers
{
    [Authorize]
    public class LocationController : Controller
    {
        private readonly IDAL _dal;
        private readonly UserManager<ApplicationUser> _usermanager;

        public LocationController(IDAL idal, UserManager<ApplicationUser> usermanager)
        {
            _dal = idal;
            _usermanager = usermanager;
        }

        // GET: Location
        public IActionResult Index()
        {
            if (TempData["Alert"] != null)
            {
                ViewData["Alert"] = TempData["Alert"];
            }
            return View(_dal.GetLocations());
        }

        // GET: Location/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = _dal.GetLocation((int)id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Location_Id,Location_Name")] Location location)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dal.CreateLocation(location);
                    TempData["Alert"] = "Success! You created a location for: " + location.Location_Name;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewData["Alert"] = "An error occurred: " + ex.Message;
                    return View(location);
                }

            }
            return View(location);
        }
    }
}
