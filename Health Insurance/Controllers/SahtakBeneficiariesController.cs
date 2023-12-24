using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health_Insurance.Models;
using Microsoft.Extensions.Hosting;

namespace Health_Insurance.Controllers
{
    public class SahtakBeneficiariesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _environment;
        public SahtakBeneficiariesController(ModelContext context)
        {
            _context = context;
        }

        // GET: SahtakBeneficiaries
        public async Task<IActionResult> Index()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account");
			}
			var modelContext = _context.SahtakBeneficiaries.Include(s => s.Subscription).Include(s => s.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: SahtakBeneficiaries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account");
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

        // GET: SahtakBeneficiaries/Create
        public async Task <IActionResult> Create()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account");
			}
			var bene = await _context.SahtakBeneficiaries.FirstOrDefaultAsync();
            var subs=await _context.SahtakSubscriptions.AsNoTracking().ToListAsync();
            var both = Tuple.Create<SahtakBeneficiary, IEnumerable<SahtakSubscription>>(bene, subs);
            ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id");
            return View(both);
        }

        // POST: SahtakBeneficiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Relationship,ProofOfRelationship,Status,SubscriptionId,FirstName,LastName,UserId,PDFFile")] SahtakBeneficiary sahtakBeneficiary,string relationship, string subscription,string fname,string lname,IFormFile fileproof)
        {
            if (ModelState.IsValid)
            {
				if (sahtakBeneficiary.PDFFile.ContentType != "application/pdf")
				{
					ModelState.AddModelError("PDFFile", "Only PDF files are allowed.");
				}
				else
				{
					if (sahtakBeneficiary.PDFFile != null && sahtakBeneficiary.PDFFile.Length > 0)
					{
						string webRootPath = _environment.WebRootPath;
						string fileName = Guid.NewGuid() + Path.GetExtension(sahtakBeneficiary.PDFFile.FileName);// Use a unique name to prevent overwriting existing files.
						string path = Path.Combine(webRootPath + "/Proof Files/" + fileName);
						try
						{
							using (var fileStream = new FileStream(path, FileMode.Create))
							{
								await sahtakBeneficiary.PDFFile.CopyToAsync(fileStream);
							}
							sahtakBeneficiary.ProofOfRelationship = fileName;
						}
						catch (IOException ex)
						{
							ViewData["Error File"] = ex.Message;
						}
					}
				}
                var newAmount = await _context.SahtakPayments.Where(x => x.UserId == HttpContext.Session.GetInt32("UserID")).FirstOrDefaultAsync();
                var beneSubscription=await _context.SahtakSubscriptions.Where(x => x.Name== subscription).AsNoTracking().FirstOrDefaultAsync();
                newAmount.Amount -= beneSubscription.Price;
				sahtakBeneficiary.Status = "Pending";
                _context.Update(newAmount);
                _context.Add(sahtakBeneficiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id", sahtakBeneficiary.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakBeneficiary.UserId);
            return View(sahtakBeneficiary);
        }

        // GET: SahtakBeneficiaries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
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

        // POST: SahtakBeneficiaries/Edit/5
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

        // GET: SahtakBeneficiaries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
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

        // POST: SahtakBeneficiaries/Delete/5
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
