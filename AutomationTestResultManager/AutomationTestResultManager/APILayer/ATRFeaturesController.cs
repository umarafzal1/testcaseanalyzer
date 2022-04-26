#nullable disable
using AutomationTestResultManager.CommonEntities;
using AutomationTestResultManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutomationTestResultManager.APILayer
{
    public class ATRFeaturesController : Controller
    {
        private readonly AutomationTestResultManagerContext _context;

        public ATRFeaturesController(AutomationTestResultManagerContext context)
        {
            _context = context;
        }

        // GET: ATRFeatures
        public async Task<IActionResult> Index()
        {
            var automationTestResultManagerContext = _context.ATRFeatures.Include(a => a.TestedInCase);
            return View(await automationTestResultManagerContext.ToListAsync());
        }

        // GET: ATRFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRFeature = await _context.ATRFeatures
                .Include(a => a.TestedInCase)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aTRFeature == null)
            {
                return NotFound();
            }

            return View(aTRFeature);
        }

        // GET: ATRFeatures/Create
        public IActionResult Create()
        {
            var selList = new SelectList(_context.ATRTestCases, "ID", "Name").ToList();
            selList.Add(new SelectListItem("None", null, true));
            ViewData["TestedInCaseId"] = selList;
            return View();
        }

        // POST: ATRFeatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,TestedInCaseId")] ATRFeature aTRFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aTRFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestedInCaseId"] = new SelectList(_context.ATRTestCases, "ID", "ID", aTRFeature.TestedInCaseId);
            return View(aTRFeature);
        }

        // GET: ATRFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRFeature = await _context.ATRFeatures.FindAsync(id);
            if (aTRFeature == null)
            {
                return NotFound();
            }
            ViewData["TestedInCaseId"] = new SelectList(_context.ATRTestCases, "ID", "ID", aTRFeature.TestedInCaseId);
            return View(aTRFeature);
        }

        // POST: ATRFeatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,TestedInCaseId")] ATRFeature aTRFeature)
        {
            if (id != aTRFeature.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aTRFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ATRFeatureExists(aTRFeature.ID))
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
            ViewData["TestedInCaseId"] = new SelectList(_context.ATRTestCases, "ID", "ID", aTRFeature.TestedInCaseId);
            return View(aTRFeature);
        }

        // GET: ATRFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRFeature = await _context.ATRFeatures
                .Include(a => a.TestedInCase)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aTRFeature == null)
            {
                return NotFound();
            }

            return View(aTRFeature);
        }

        // POST: ATRFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aTRFeature = await _context.ATRFeatures.FindAsync(id);
            _context.ATRFeatures.Remove(aTRFeature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ATRFeatureExists(int id)
        {
            return _context.ATRFeatures.Any(e => e.ID == id);
        }
    }
}
