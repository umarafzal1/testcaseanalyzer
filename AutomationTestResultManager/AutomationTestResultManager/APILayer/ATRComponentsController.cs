#nullable disable
using AutomationTestResultManager.CommonEntities;
using AutomationTestResultManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutomationTestResultManager.APILayer
{
    public class ATRComponentsController : Controller
    {
        private readonly AutomationTestResultManagerContext _context;

        public ATRComponentsController(AutomationTestResultManagerContext context)
        {
            _context = context;
        }

        // GET: ATRComponents
        public async Task<IActionResult> Index()
        {
            return View(await _context.ATRComponents.ToListAsync());
        }

        // GET: ATRComponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRComponent = await _context.ATRComponents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aTRComponent == null)
            {
                return NotFound();
            }

            return View(aTRComponent);
        }

        // GET: ATRComponents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ATRComponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] ATRComponent aTRComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aTRComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aTRComponent);
        }

        // GET: ATRComponents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRComponent = await _context.ATRComponents.FindAsync(id);
            if (aTRComponent == null)
            {
                return NotFound();
            }
            return View(aTRComponent);
        }

        // POST: ATRComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] ATRComponent aTRComponent)
        {
            if (id != aTRComponent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aTRComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ATRComponentExists(aTRComponent.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aTRComponent);
        }

        // GET: ATRComponents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRComponent = await _context.ATRComponents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aTRComponent == null)
            {
                return NotFound();
            }

            return View(aTRComponent);
        }

        // POST: ATRComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aTRComponent = await _context.ATRComponents.FindAsync(id);
            _context.ATRComponents.Remove(aTRComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ATRComponentExists(int id)
        {
            return _context.ATRComponents.Any(e => e.ID == id);
        }
    }
}
