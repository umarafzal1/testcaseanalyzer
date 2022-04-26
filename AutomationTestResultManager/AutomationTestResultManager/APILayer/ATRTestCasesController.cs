#nullable disable
using AutomationTestResultManager.CommonEntities;
using AutomationTestResultManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutomationTestResultManager.APILayer
{
    public class ATRTestCasesController : Controller
    {
        private readonly AutomationTestResultManagerContext _context;

        public ATRTestCasesController(AutomationTestResultManagerContext context)
        {
            _context = context;
        }

        // GET: ATRTestCases
        public async Task<IActionResult> Index()
        {
            var automationTestResultManagerContext = _context.ATRTestCases.Include(a => a.MasterComponent);
            var list = (await automationTestResultManagerContext.ToListAsync());

            list.ForEach(x =>
            {
                if (!string.IsNullOrWhiteSpace(x.TestStatus))
                {
                    if (x.TestStatus.Equals("pass", StringComparison.InvariantCultureIgnoreCase))
                        x.RowColor = "greentest";
                    if (x.TestStatus.Equals("fail", StringComparison.InvariantCultureIgnoreCase))
                        x.RowColor = "redtest";
                }


            });
            return View(list);

        }

        // GET: ATRTestCases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRTestCase = await _context.ATRTestCases
                .Include(a => a.MasterComponent)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aTRTestCase == null)
            {
                return NotFound();
            }

            return View(aTRTestCase);
        }

        // GET: ATRTestCases/Create
        public IActionResult Create()
        {
            var selList = new SelectList(_context.ATRComponents, "ID", "Name").ToList();
            selList.Add(new SelectListItem("None", null, true));
            ViewData["MasterComponentId"] = selList;

            return View();
        }

        // POST: ATRTestCases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,TestedBy,TestStatus,TestDate,MasterComponentId")] ATRTestCase aTRTestCase)
        {
            ModelState.Remove("RowColor");
            if (ModelState.IsValid)
            {
                _context.Add(aTRTestCase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MasterComponentId"] = new SelectList(_context.ATRComponents, "ID", "ID", aTRTestCase.MasterComponentId);
            return View(aTRTestCase);
        }

        // GET: ATRTestCases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRTestCase = await _context.ATRTestCases.FindAsync(id);
            if (aTRTestCase == null)
            {
                return NotFound();
            }
            ViewData["MasterComponentId"] = new SelectList(_context.ATRComponents, "ID", "ID", aTRTestCase.MasterComponentId);
            return View(aTRTestCase);
        }

        // POST: ATRTestCases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,TestedBy,TestStatus,TestDate,MasterComponentId")] ATRTestCase aTRTestCase)
        {
            if (id != aTRTestCase.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aTRTestCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ATRTestCaseExists(aTRTestCase.ID))
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
            ViewData["MasterComponentId"] = new SelectList(_context.ATRComponents, "ID", "ID", aTRTestCase.MasterComponentId);
            return View(aTRTestCase);
        }

        // GET: ATRTestCases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRTestCase = await _context.ATRTestCases
                .Include(a => a.MasterComponent)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (aTRTestCase == null)
            {
                return NotFound();
            }

            return View(aTRTestCase);
        }

        // POST: ATRTestCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aTRTestCase = await _context.ATRTestCases.FindAsync(id);
            _context.ATRTestCases.Remove(aTRTestCase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ATRTestCaseExists(int id)
        {
            return _context.ATRTestCases.Any(e => e.ID == id);
        }
    }
}
