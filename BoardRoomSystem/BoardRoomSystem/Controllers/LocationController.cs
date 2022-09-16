using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BoardRoomSystem.Models;
using BoardRoomSystem.Areas.Identity.Data;

namespace BoardRoomSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext dBContext;

        public LocationController(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IActionResult>Index()
        {
            var applicationDbContext = dBContext.Locations.Include(a => a.IdLocation);
            return View(await dBContext.Locations.ToListAsync());
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var areasViewModel = await dBContext.Locations.FirstOrDefaultAsync(a => a.IdLocation == Id);

            if (areasViewModel == null)
            {
                return NotFound();
            }

            return View(areasViewModel);
        }

        //Crear por medio de vista

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Location locations)
        {
            if (ModelState.IsValid)
            {
                dBContext.Add(locations);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(locations);
        }

        //// GET: Location
        //public IActionResult Index()
        //{
        //    if (TempData["Alert"] != null)
        //    {
        //        ViewData["Alert"] = TempData["Alert"];
        //    }
        //    return View(_dal.GetLocations());
        //}

        //// GET: Location/Details/5
        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var location = _dal.GetLocation((int)id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(location);
        //}

        //// GET: Location/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Location/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create([Bind("IdLocation,NameLocation")] Location location)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _dal.CreateLocation(location);
        //            TempData["Alert"] = "Success! You created a location for: " + location.NameLocation;
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewData["Alert"] = "An error occurred: " + ex.Message;
        //            return View(location);
        //        }

        //    }
        //    return View(location);
        //}
    }
}
