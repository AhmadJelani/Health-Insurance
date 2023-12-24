using Health_Insurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Health_Insurance.Areas.AdminDashboard.Controllers
{
	[Area("AdminDashboard")]
	public class HomeController : Controller
    {
		private readonly ModelContext _context;
        public HomeController(ModelContext context)
        {
			_context = context;
        }
        public IActionResult Index()
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area=""});
			}
            ViewData["TotalUsers"] = _context.SahtakUsers.Count();
            ViewData["NumberOfSubs"]=_context.SahtakSubscriptions.Count();
			return View();
        }
        public async Task <IActionResult> Report() {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
            ViewData["LastName"] = HttpContext.Session.GetString("LastName");
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("SignIn", "Account", new { area = "" });
            }

            var user=await _context.SahtakUsers.AsNoTracking().ToListAsync();
			var subscriptions=await _context.SahtakSubscriptions.AsNoTracking().ToListAsync();
			var subscribe=await _context.UserSubscriptions.AsNoTracking().ToListAsync();
			var pay=await _context.SahtakPayments.AsNoTracking().ToListAsync();

			var all = from p in pay
					  join s in subscriptions on p.SubscriptionId equals s.Id
					  join us in subscribe on s.Id equals us.SubscriptionId
					  join u in user on p.UserId equals u.Id
					  select new ReportJoinTable { 
						  pay=p,user=u,subs=s,subscribe=us
					  };

			return View(all);
		}

        [HttpPost]
		public async Task<IActionResult> Report(DateTime? DateFrom,DateTime? DateTo) 
        {
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account", new { area = "" });
			}
			var user = await _context.SahtakUsers.AsNoTracking().ToListAsync();
            var subscriptions = await _context.SahtakSubscriptions.AsNoTracking().ToListAsync();
            var subscribe = await _context.UserSubscriptions.AsNoTracking().ToListAsync();
            var pay = await _context.SahtakPayments.AsNoTracking().ToListAsync();

            var Join = from p in pay
                       join s in subscriptions on p.SubscriptionId equals s.Id
                       join us in subscribe on s.Id equals us.SubscriptionId
                       join u in user on p.UserId equals u.Id
                       select new ReportJoinTable
                       {
                           pay = p,
                           user = u,
                           subs = s,
                           subscribe = us
                       };

            if (DateFrom == null && DateTo == null) {
                ViewData["TotalPay"] = Join.Sum(x => x.subs.Price);
                return View(Join);
            }
            else if (DateFrom != null && DateTo == null) {
                ViewData["TotalPay"] = Join.Where(x=>x.subscribe.DateFrom.Value.Date==DateFrom).Sum(x => x.subs.Price);
                return View(Join
					.Where(x => x.subscribe.DateFrom.Value.Date == DateFrom));
            }
            else if (DateFrom == null && DateTo != null) {
                ViewData["TotalPay"] = Join.Where(x => x.subscribe.DateTo.Value.Date == DateTo).Sum(x => x.subs.Price);
                return View(Join
                    .Where(x => x.subscribe.DateTo.Value.Date == DateTo));
            }
            else {
                ViewData["TotalPay"] = Join.Where(x => x.subscribe.DateFrom.Value.Date == DateFrom
                && x.subscribe.DateTo.Value.Date==DateTo).Sum(x => x.subs.Price);

                return View(Join
                   .Where(x => x.subscribe.DateFrom.Value.Date >= DateFrom&& x.subscribe.DateTo.Value.Date >= DateTo));
            }

        }
    }
}
