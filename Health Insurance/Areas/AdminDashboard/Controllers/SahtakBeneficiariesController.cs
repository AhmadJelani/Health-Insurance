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
    public class SahtakBeneficiariesController : Controller
    {
        private readonly ModelContext _context;

        public SahtakBeneficiariesController(ModelContext context)
        {
            _context = context;
        }

        // GET: AdminDashboard/SahtakBeneficiaries
        public async Task<IActionResult> Index()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			var modelContext = _context.SahtakBeneficiaries.Include(s => s.Subscription).Include(s => s.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: AdminDashboard/SahtakBeneficiaries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakBeneficiaries == null)
            {
                return NotFound();
            }

            var sahtakBeneficiary = await _context.SahtakBeneficiaries
                .Include(s => s.Subscription)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakBeneficiary == null)
            {
                return NotFound();
            }

            return View(sahtakBeneficiary);
        }

        // GET: AdminDashboard/SahtakBeneficiaries/Create
        public IActionResult Create()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id");
            return View();
        }

        // POST: AdminDashboard/SahtakBeneficiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Relationship,ProofOfRelationship,Status,SubscriptionId,FirstName,LastName,UserId")] SahtakBeneficiary sahtakBeneficiary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sahtakBeneficiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id", sahtakBeneficiary.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakBeneficiary.UserId);
            return View(sahtakBeneficiary);
        }

        // GET: AdminDashboard/SahtakBeneficiaries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakBeneficiaries == null)
            {
                return NotFound();
            }

            var sahtakBeneficiary = await _context.SahtakBeneficiaries.FindAsync(id);
            if (sahtakBeneficiary == null)
            {
                return NotFound();
            }
            ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id", sahtakBeneficiary.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakBeneficiary.UserId);
            return View(sahtakBeneficiary);
        }

        // POST: AdminDashboard/SahtakBeneficiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Relationship,ProofOfRelationship,Status,SubscriptionId,FirstName,LastName,UserId")] SahtakBeneficiary sahtakBeneficiary)
        {
            if (id != sahtakBeneficiary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sahtakBeneficiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SahtakBeneficiaryExists(sahtakBeneficiary.Id))
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
            ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id", sahtakBeneficiary.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakBeneficiary.UserId);
            return View(sahtakBeneficiary);
        }

        // GET: AdminDashboard/SahtakBeneficiaries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakBeneficiaries == null)
            {
                return NotFound();
            }

            var sahtakBeneficiary = await _context.SahtakBeneficiaries
                .Include(s => s.Subscription)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakBeneficiary == null)
            {
                return NotFound();
            }

            return View(sahtakBeneficiary);
        }

        // POST: AdminDashboard/SahtakBeneficiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.SahtakBeneficiaries == null)
            {
                return Problem("Entity set 'ModelContext.SahtakBeneficiaries'  is null.");
            }
            var sahtakBeneficiary = await _context.SahtakBeneficiaries.FindAsync(id);
            if (sahtakBeneficiary != null)
            {
                _context.SahtakBeneficiaries.Remove(sahtakBeneficiary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SahtakBeneficiaryExists(decimal id)
        {
          return (_context.SahtakBeneficiaries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
