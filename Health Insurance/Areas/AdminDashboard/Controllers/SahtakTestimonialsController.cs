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
    public class SahtakTestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public SahtakTestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: AdminDashboard/SahtakTestimonials
        public async Task<IActionResult> Index()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			var modelContext = _context.SahtakTestimonials.Include(s => s.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: AdminDashboard/SahtakTestimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakTestimonials == null)
            {
                return NotFound();
            }

            var sahtakTestimonial = await _context.SahtakTestimonials
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakTestimonial == null)
            {
                return NotFound();
            }

            return View(sahtakTestimonial);
        }

        // GET: AdminDashboard/SahtakTestimonials/Edit/5
        public async Task<IActionResult> Approve(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakTestimonials == null)
            {
                return NotFound();
            }

            var sahtakTestimonial = await _context.SahtakTestimonials.FindAsync(id);
            if (sahtakTestimonial == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakTestimonial.UserId);
            return View(sahtakTestimonial);
        }

        // POST: AdminDashboard/SahtakTestimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(decimal id, [Bind("Id,Text,Status,SubmitDate,UserId")] SahtakTestimonial sahtakTestimonial)
        {
            if (id != sahtakTestimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sahtakTestimonial.Status = "Approved";
                    _context.Update(sahtakTestimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SahtakTestimonialExists(sahtakTestimonial.Id))
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
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakTestimonial.UserId);
            return View(sahtakTestimonial);
        }

        // GET: AdminDashboard/SahtakTestimonials/Edit/5
        public async Task<IActionResult> Reject(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.SahtakTestimonials == null)
            {
                return NotFound();
            }

            var sahtakTestimonial = await _context.SahtakTestimonials.FindAsync(id);
            if (sahtakTestimonial == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakTestimonial.UserId);
            return View(sahtakTestimonial);
        }

        // POST: AdminDashboard/SahtakTestimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(decimal id, [Bind("Id,Text,Status,SubmitDate,UserId")] SahtakTestimonial sahtakTestimonial)
        {
            if (id != sahtakTestimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sahtakTestimonial.Status = "Rejected";
                    _context.Update(sahtakTestimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SahtakTestimonialExists(sahtakTestimonial.Id))
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
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakTestimonial.UserId);
            return View(sahtakTestimonial);
        }

        // GET: AdminDashboard/SahtakTestimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			if (id == null || _context.SahtakTestimonials == null)
            {
                return NotFound();
            }

            var sahtakTestimonial = await _context.SahtakTestimonials
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sahtakTestimonial == null)
            {
                return NotFound();
            }

            return View(sahtakTestimonial);
        }

        // POST: AdminDashboard/SahtakTestimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.SahtakTestimonials == null)
            {
                return Problem("Entity set 'ModelContext.SahtakTestimonials'  is null.");
            }
            var sahtakTestimonial = await _context.SahtakTestimonials.FindAsync(id);
            if (sahtakTestimonial != null)
            {
                _context.SahtakTestimonials.Remove(sahtakTestimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SahtakTestimonialExists(decimal id)
        {
          return (_context.SahtakTestimonials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
