using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BoardRoomSystem.Controllers
{
    //[Authorize(Policy = "RequireAdmin")]
    [Authorize(Roles = "Admin")]
    public class AreasController : Controller
    {
        private readonly ApplicationDbContext dBContext;

        public AreasController(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = dBContext.AreasViewModels.Include(a => a.Area_Id);
            return View(await dBContext.AreasViewModels.ToListAsync());
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var areasViewModel = await dBContext.AreasViewModels.FirstOrDefaultAsync(a => a.Area_Id == Id);

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
        public async Task<IActionResult> Create(AreasViewModel areasViewModel)
        {
            if (ModelState.IsValid)
            {
                dBContext.Add(areasViewModel);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(areasViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areasViewModel = await dBContext.AreasViewModels.FindAsync(id);

            if (areasViewModel == null)
            {
                return NotFound();
            }

            return View(areasViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AreasViewModel areasViewModel)
        {
            if (id != areasViewModel.Area_Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    dBContext.Update(areasViewModel);
                    await dBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(areasViewModel);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var areasViewModel = await dBContext.AreasViewModels.FirstOrDefaultAsync(a => a.Area_Id == id);

            if (areasViewModel == null)
            {
                return NotFound();
            }

            return View(areasViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) 
        {
            var areasViewModel = await dBContext.AreasViewModels.FindAsync(id);
            dBContext.AreasViewModels.Remove(areasViewModel);
            await dBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
