using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health_Insurance.Models;
using Microsoft.Extensions.Hosting;

namespace Health_Insurance.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    public class HomePagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;
        public HomePagesController(ModelContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AdminDashboard/HomePages1
        public async Task<IActionResult> Index()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            return _context.HomePages != null ? 
                          View(await _context.HomePages.ToListAsync()) :
                          Problem("Entity set 'ModelContext.HomePages'  is null.");
        }

        // GET: AdminDashboard/HomePages1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.HomePages == null)
            {
                return NotFound();
            }

            var homePage = await _context.HomePages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homePage == null)
            {
                return NotFound();
            }

            return View(homePage);
        }


        // GET: AdminDashboard/HomePages1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }
            if (id == null || _context.HomePages == null)
            {
                return NotFound();
            }

            var homePage = await _context.HomePages.FindAsync(id);
            if (homePage == null)
            {
                return NotFound();
            }
            return View(homePage);
        }

        // POST: AdminDashboard/HomePages1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,BgImageOne,Bg_ImageOne,BgImageTwo,Bg_ImageTwo,BgImageThree,Bg_ImageThree,BgImageFour,Bg_ImageFour,BgImageFive,Bg_ImageFive,TitleCardOne,TextCardOne,TitleCardThree,TextCardThree,IntroTitle,IntroText,PointOneTitle,PointOneText,PointTwoTitle,PointTwoText,PointThreeTitle,PointThreeText,TitleEmer,TextEmer,TitleSubs,TextSubs,Feedback")] HomePage homePage)
        {
            if (id != homePage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (homePage.Bg_ImageOne != null)
                    {
                        string wwwRootPath = _environment.WebRootPath;

                        string File_Name = Guid.NewGuid().ToString() + homePage.Bg_ImageOne.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/" + File_Name);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await homePage.Bg_ImageOne.CopyToAsync(fileStream);
                        }
                        homePage.BgImageOne = File_Name;
                    }
                    if (homePage.Bg_ImageTwo != null)
                    {
                        string wwwRootPath = _environment.WebRootPath;

                        string File_Name = Guid.NewGuid().ToString() + homePage.Bg_ImageTwo.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/" + File_Name);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await homePage.Bg_ImageTwo.CopyToAsync(fileStream);
                        }
                        homePage.BgImageTwo = File_Name;
                    }
                    if (homePage.Bg_ImageThree != null)
                    {
                        string wwwRootPath = _environment.WebRootPath;

                        string File_Name = Guid.NewGuid().ToString() + homePage.Bg_ImageThree.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/" + File_Name);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await homePage.Bg_ImageThree.CopyToAsync(fileStream);
                        }
                        homePage.BgImageThree = File_Name;
                    }
                    if (homePage.Bg_ImageFour != null)
                    {
                        string wwwRootPath = _environment.WebRootPath;

                        string File_Name = Guid.NewGuid().ToString() + homePage.Bg_ImageFour.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/" + File_Name);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await homePage.Bg_ImageFour.CopyToAsync(fileStream);
                        }
                        homePage.BgImageFour = File_Name;
                    }
                    if (homePage.Bg_ImageFive != null)
                    {
                        string wwwRootPath = _environment.WebRootPath;

                        string File_Name = Guid.NewGuid().ToString() + homePage.Bg_ImageFive.FileName;

                        string path = Path.Combine(wwwRootPath + "/Images/" + File_Name);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await homePage.Bg_ImageFive.CopyToAsync(fileStream);
                        }
                        homePage.BgImageFive = File_Name;
                    }
                    _context.Update(homePage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomePageExists(homePage.Id))
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
            return View(homePage);
        }

        private bool HomePageExists(decimal id)
        {
          return (_context.HomePages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
