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
    public class SahtakPaymentsController : Controller
    {
        private readonly ModelContext _context;

        public SahtakPaymentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: AdminDashboard/SahtakPayments
        public async Task<IActionResult> Index()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			var modelContext = _context.SahtakPayments.Include(s => s.Bank).Include(s => s.Subscription).Include(s => s.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: AdminDashboard/SahtakPayments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakPayments == null)
            {
                return NotFound();
            }

            var sahtakPayment = await _context.SahtakPayments
                .Include(s => s.Bank)
                .Include(s => s.Subscription)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakPayment == null)
            {
                return NotFound();
            }

            return View(sahtakPayment);
        }
        
    }
}
