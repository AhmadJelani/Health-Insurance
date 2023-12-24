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
    public class ContactUsPagesController : Controller
    {
        private readonly ModelContext _context;

        public ContactUsPagesController(ModelContext context)
        {
            _context = context;
        }

        // GET: AdminDashboard/ContactUsPages
        public async Task<IActionResult> Index()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            return _context.ContactUsPages != null ? 
                          View(await _context.ContactUsPages.ToListAsync()) :
                          Problem("Entity set 'ModelContext.ContactUsPages'  is null.");
        }

        // GET: AdminDashboard/ContactUsPages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.ContactUsPages == null)
            {
                return NotFound();
            }

            var contactUsPage = await _context.ContactUsPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactUsPage == null)
            {
                return NotFound();
            }

            return View(contactUsPage);
        }

        // GET: AdminDashboard/ContactUsPages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.ContactUsPages == null)
            {
                return NotFound();
            }

            var contactUsPage = await _context.ContactUsPages.FindAsync(id);
            if (contactUsPage == null)
            {
                return NotFound();
            }
            return View(contactUsPage);
        }

        // POST: AdminDashboard/ContactUsPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Title,Header,Paragraph,PhoneNumber,Location,LocationDesc,WorkingDays,WorkingDaysDesc")] ContactUsPage contactUsPage)
        {
            if (id != contactUsPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactUsPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactUsPageExists(contactUsPage.Id))
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
            return View(contactUsPage);
        }

        private bool ContactUsPageExists(decimal id)
        {
          return (_context.ContactUsPages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
