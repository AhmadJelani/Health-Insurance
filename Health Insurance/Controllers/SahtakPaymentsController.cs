using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health_Insurance.Models;

namespace Health_Insurance.Controllers
{
    public class SahtakPaymentsController : Controller
    {
        private readonly ModelContext _context;

        public SahtakPaymentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: SahtakPayments
        public async Task<IActionResult> Index()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            var modelContext = _context.SahtakPayments.Include(s => s.Bank).Include(s => s.Subscription).Include(s => s.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: SahtakPayments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account");
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

        // GET: SahtakPayments/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            return View();
        }

        // POST: SahtakPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CardNameholder,CardNumber,Status,PaymentDate,UserId,BankId,SubscriptionId")] SahtakPayment sahtakPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sahtakPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankId"] = new SelectList(_context.SahtakBanks, "Id", "Id", sahtakPayment.BankId);
            ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id", sahtakPayment.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakPayment.UserId);
            return View(sahtakPayment);
        }

        // GET: SahtakPayments/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            if (id == null || _context.SahtakPayments == null)
            {
                return NotFound();
            }

            var sahtakPayment = await _context.SahtakPayments.FindAsync(id);
            if (sahtakPayment == null)
            {
                return NotFound();
            }
            ViewData["BankId"] = new SelectList(_context.SahtakBanks, "Id", "Id", sahtakPayment.BankId);
            ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id", sahtakPayment.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakPayment.UserId);
            return View(sahtakPayment);
        }

        // POST: SahtakPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CardNameholder,CardNumber,Status,PaymentDate,UserId,BankId,SubscriptionId")] SahtakPayment sahtakPayment)
        {
            if (id != sahtakPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sahtakPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SahtakPaymentExists(sahtakPayment.Id))
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
            ViewData["BankId"] = new SelectList(_context.SahtakBanks, "Id", "Id", sahtakPayment.BankId);
            ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id", sahtakPayment.SubscriptionId);
            ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakPayment.UserId);
            return View(sahtakPayment);
        }

        // GET: SahtakPayments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account");
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

        // POST: SahtakPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.SahtakPayments == null)
            {
                return Problem("Entity set 'ModelContext.SahtakPayments'  is null.");
            }
            var sahtakPayment = await _context.SahtakPayments.FindAsync(id);
            if (sahtakPayment != null)
            {
                _context.SahtakPayments.Remove(sahtakPayment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SahtakPaymentExists(decimal id)
        {
          return (_context.SahtakPayments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
