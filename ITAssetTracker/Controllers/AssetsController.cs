
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITAssetTracker.Models;
using ITAssetTracker.Data;

public class AssetsController : Controller
{
    private readonly AppDbContext _context;

    public AssetsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: ASSETS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Assets.ToListAsync());
    }

    // GET: ASSETS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var asset = await _context.Assets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (asset == null)
        {
            return NotFound();
        }

        return View(asset);
    }

    // GET: ASSETS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ASSETS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Type,Status,AssignedTo,Location,Tickets")] Asset asset)
    {
        if (ModelState.IsValid)
        {
            _context.Add(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(asset);
    }

    // GET: ASSETS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var asset = await _context.Assets.FindAsync(id);
        if (asset == null)
        {
            return NotFound();
        }
        return View(asset);
    }

    // POST: ASSETS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Type,Status,AssignedTo,Location,Tickets")] Asset asset)
    {
        if (id != asset.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(asset);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(asset.Id))
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
        return View(asset);
    }

    // GET: ASSETS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var asset = await _context.Assets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (asset == null)
        {
            return NotFound();
        }

        return View(asset);
    }

    // POST: ASSETS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var asset = await _context.Assets.FindAsync(id);
        if (asset != null)
        {
            _context.Assets.Remove(asset);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool AssetExists(int? id)
    {
        return _context.Assets.Any(e => e.Id == id);
    }
}
