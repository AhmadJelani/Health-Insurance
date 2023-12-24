using Health_Insurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace Sahtak.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ModelContext _context;

		public HomeController(ILogger<HomeController> logger, ModelContext context)
		{
			_logger = logger;
			_context = context;
		}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task <IActionResult> Index()
		{
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			var subs = await _context.SahtakSubscriptions.AsNoTracking().Take(3).ToListAsync();
			var feedback = await _context.SahtakTestimonials.Where(x => x.Status == "Approved").AsNoTracking().ToListAsync();
			var users = await _context.SahtakUsers.AsNoTracking().ToListAsync();
			var page = await _context.HomePages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 5);
			var join = from f in feedback join u in users on f.UserId equals u.Id select new FeedbackJooinTable {
				feedback = f, user = u
			};

			var all = Tuple.Create<IEnumerable<SahtakSubscription>, IEnumerable<FeedbackJooinTable>,HomePage>(subs,join,page);
            return View(all);
		}

		
		public async Task <IActionResult> About(int id=8)
		{
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (id == null || _context.AboutUsPages == null)
            {
                return NotFound();
            }

            var aboutUsPage = await _context.AboutUsPages.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == 8);
            if (aboutUsPage == null)
            {
                return NotFound();
            }

            return View(aboutUsPage);
		}
		public async Task <IActionResult> Testimonial(int id = 1)
		{
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account");
			}
			if (id == null || _context.TestimonialPages == null)
			{
				return NotFound();
			}

			var testimonialPage = await _context.TestimonialPages.AsNoTracking()
				.FirstOrDefaultAsync(m => m.Id == id);
			if (testimonialPage == null)
			{
				return NotFound();
			}
			var test= await _context.SahtakTestimonials.AsNoTracking().FirstOrDefaultAsync();
			var both = Tuple.Create<TestimonialPage,SahtakTestimonial>(testimonialPage,test);
			return View(both);

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Testimonial([Bind("Id,Text,Status,SubmitDate,UserId")] SahtakTestimonial sahtakTestimonial,string Text)
		{
			if (ModelState.IsValid)
			{
				sahtakTestimonial.SubmitDate=DateTime.Now;
				sahtakTestimonial.UserId= HttpContext.Session.GetInt32("UserID");
				sahtakTestimonial.Status = "Pending";
				sahtakTestimonial.Text= Text;
				_context.Add(sahtakTestimonial);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakTestimonial.UserId);
			return View(sahtakTestimonial);
		}
		public async Task <IActionResult> ContactUs()
		{
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");

			var contactUsPage = await _context.ContactUsPages
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == 1);

			var cont = await _context.SahtakContactUs.FirstOrDefaultAsync(x=>x.Id==2);
			var both=Tuple.Create<ContactUsPage,SahtakContactU>(contactUsPage, cont);
			return View(both);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ContactUs([Bind("Id,Name,Email,PhoneNumber,Subject,Description")] SahtakContactU SahtakContactUs)
		{
			if (ModelState.IsValid)
			{
				_context.SahtakContactUs.Add(SahtakContactUs);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(SahtakContactUs);
		}
		public async Task <IActionResult> AddBeneficiaries()
		{
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			return View();
		}
	}
}