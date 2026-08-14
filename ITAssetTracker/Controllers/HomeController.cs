using ITAssetTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ITAssetTracker.Data;

namespace ITAssetTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["TotalAssets"] = await _context.Assets.CountAsync();
            ViewData["AvailableAssets"] = await _context.Assets.CountAsync(a => a.Status == "Available");
            ViewData["OpenTickets"] = await _context.Tickets.CountAsync(t => t.Status == "Open");
            ViewData["TotalTickets"] = await _context.Tickets.CountAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
