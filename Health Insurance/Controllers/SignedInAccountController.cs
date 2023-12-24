using Health_Insurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Sahtak.Controllers
{
	public class SignedInAccountController : Controller
	{
		private readonly ModelContext _context;
		public SignedInAccountController(ModelContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> MyAccount(decimal? id)
		{
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account");
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
		// GET: Dashboard/SahtakUsers/Edit/5
		public async Task<IActionResult> Edit(decimal? id)
		{
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account");
			}
			if (id == null || _context.SahtakUsers == null)
			{
				return NotFound();
			}

			var sahtakUser = await _context.SahtakUsers.FindAsync(id);
			if (sahtakUser == null)
			{
				return NotFound();
			}
			ViewData["RoleId"] = new SelectList(_context.SahtakRoles, "Id", "Id", sahtakUser.RoleId);
			return View(sahtakUser);
		}

		// POST: Dashboard/SahtakUsers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(decimal id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,Address,Password")] SahtakUser sahtakUser,string date)
		{
			var user = _context.SahtakUsers.Where(x => x.Id == sahtakUser.Id);
			if (id != sahtakUser.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					sahtakUser.RoleId = 2;
					sahtakUser.JoinDate = DateTime.Parse(date);
					_context.Update(sahtakUser);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SahtakUserExists(sahtakUser.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index), "Home");
			}
			ViewData["RoleId"] = new SelectList(_context.SahtakRoles, "Id", "Id", sahtakUser.RoleId);
			return View(sahtakUser);
		}
		private bool SahtakUserExists(decimal id)
		{
			return (_context.SahtakUsers?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
