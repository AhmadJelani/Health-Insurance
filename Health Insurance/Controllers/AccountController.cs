using Health_Insurance.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Sahtak.Controllers
{
	public class AccountController : Controller
	{
		private readonly ModelContext _context;
		public AccountController(ModelContext context)
		{
			_context = context;
		}
		public IActionResult Register()
		{
			return View();
		}
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register([Bind("Id,FirstName,LastName,Email,PhoneNumber,Address,Password")] SahtakUser sahtakUser,string ConfirmPassword)
		{
			if (ModelState.IsValid)
			{
                var ValidEmail=await _context.SahtakUsers.Where(x=>x.Email==sahtakUser.Email).AsNoTracking().FirstOrDefaultAsync();
                if (ValidEmail == null)
				{
					if (sahtakUser.Password!=ConfirmPassword) {
						ViewData["ConfrimPasswordWrong"] = "Please make sure that the password and confirm password are the same";
						return View(sahtakUser);
					}
					sahtakUser.JoinDate = DateTime.Now;
					sahtakUser.RoleId = 2;
					_context.Add(sahtakUser);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(SignIn));
				}
				else
				{
					ViewData["InvalidEmail"] = "Email is Used";
					return View(sahtakUser);
				}
			}
			return View(sahtakUser);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn([Bind("Id,Email,Password")] SahtakUser sahtakUser)
		{
			var ValidUser = _context.SahtakUsers.Where(x => x.Email == sahtakUser.Email && x.Password == sahtakUser.Password).FirstOrDefault();
			if (ValidUser != null)
			{
				switch (ValidUser.RoleId)
				{
					case 1:
						HttpContext.Session.SetInt32("UserID", (int)ValidUser.Id);
						HttpContext.Session.SetString("FirstName", ValidUser.FirstName);
						HttpContext.Session.SetString("LastName", ValidUser.LastName);
						return RedirectToAction("Index", "Home", new { area = "AdminDashboard" });
					case 2:
						HttpContext.Session.SetInt32("UserID", (int)ValidUser.Id);
						HttpContext.Session.SetString("FirstName", ValidUser.FirstName);
						HttpContext.Session.SetString("LastName", ValidUser.LastName);
						return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				ViewData["ErrorSignIn"] = "Invalid Email Or Password";
			}
			return View(sahtakUser);
		}
		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}
