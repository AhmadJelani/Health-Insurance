using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health_Insurance.Models;

namespace WebApplication1.Controllers
{
	[Area("AdminDashboard")]
	public class AboutUsPagesController : Controller
    {
        private readonly ModelContext _context;

        public AboutUsPagesController(ModelContext context)
        {
            _context = context;
        }

        // GET: AboutUsPages
        public async Task<IActionResult> Index()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            return _context.AboutUsPages != null ? 
                          View(await _context.AboutUsPages.ToListAsync()) :
                          Problem("Entity set 'ModelContext.AboutUsPages'  is null.");
        }

        // GET: AboutUsPages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.AboutUsPages == null)
            {
                return NotFound();
            }

            var aboutUsPage = await _context.AboutUsPages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUsPage == null)
            {
                return NotFound();
            }

            return View(aboutUsPage);
        }

        // GET: AboutUsPages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.AboutUsPages == null)
            {
                return NotFound();
            }

            var aboutUsPage = await _context.AboutUsPages.FindAsync(id);
            if (aboutUsPage == null)
            {
                return NotFound();
            }
            return View(aboutUsPage);
        }

        // POST: AboutUsPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,FirstTitle,FirstParagraph,ImagePath,SecondTitle,SecondParagraph,SixPointParagraph,PointOne,PointTwo,PointThree,PointFour,PointFive,PointSix,ThirdTitle,ThirdParagraph,PointOneE,PointOneEText,PointTwoE,PointTwoEText,PointThreeE,PointThreeEText,PointFourE,PointFiveEText,PointSixE,PointSixEText,PointFourEText,PointFiveE")] AboutUsPage aboutUsPage)
        {
            if (id != aboutUsPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
                    ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
                    ViewData["LastName"] = HttpContext.Session.GetString("LastName");
                    if (HttpContext.Session.GetInt32("UserID") == null)
                    {
                        return RedirectToAction("SignIn", "Account", new { area = "" });
                    }
                    _context.Update(aboutUsPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsPageExists(aboutUsPage.Id))
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
            return View(aboutUsPage);
        }

        private bool AboutUsPageExists(decimal id)
        {
          return (_context.AboutUsPages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
