using BoardRoomSystem.Areas.Identity.Data;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BoardRoomSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatesController : Controller
    {
        private readonly ApplicationDbContext dBContext;

        public StatesController(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = dBContext.States.Include(s => s.State_Id);
            return View(await dBContext.States.ToListAsync());
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var statesViewModel = await dBContext.States.FirstOrDefaultAsync(s => s.State_Id == Id);

            if (statesViewModel == null)
            {
                return NotFound();
            }

            return View(statesViewModel);
        }

        //Crear por medio de vista

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(State statesViewModel)
        {
            if (ModelState.IsValid)
            {
                dBContext.Add(statesViewModel);
                await dBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            return View(statesViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statesViewModel = await dBContext.States.FindAsync(id);

            if (statesViewModel == null)
            {
                return NotFound();
            }

            return View(statesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, State statesViewModel)
        {
            if (id != statesViewModel.State_Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    dBContext.Update(statesViewModel);
                    await dBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(statesViewModel);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var statesViewModel = await dBContext.States.FirstOrDefaultAsync(s => s.State_Id == id);

            if (statesViewModel == null)
            {
                return NotFound();
            }

            return View(statesViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var statesViewModel = await dBContext.States.FindAsync(id);
            dBContext.States.Remove(statesViewModel);
            await dBContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
