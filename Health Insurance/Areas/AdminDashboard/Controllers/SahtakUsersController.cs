using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health_Insurance.Models;

namespace Health_Insurance.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    public class SahtakUsersController : Controller
    {
        private readonly ModelContext _context;

        public SahtakUsersController(ModelContext context)
        {
            _context = context;
        }

        // GET: AdminDashboard/SahtakUsers
        public async Task<IActionResult> Index()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			var modelContext = _context.SahtakUsers.Include(s => s.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: AdminDashboard/SahtakUsers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakUsers == null)
            {
                return NotFound();
            }

            var sahtakUser = await _context.SahtakUsers
                .Include(s => s.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakUser == null)
            {
                return NotFound();
            }

            return View(sahtakUser);
        }
    }
}
