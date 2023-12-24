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
    public class SahtakSubscriptionsController : Controller
    {
        private readonly ModelContext _context;

        public SahtakSubscriptionsController(ModelContext context)
        {
            _context = context;
        }

		// GET: SahtakSubscriptions
		public async Task<IActionResult> Index()
		{
			return _context.SahtakSubscriptions != null ?
						View(await _context.SahtakSubscriptions.ToListAsync()) :
						Problem("Entity set 'ModelContext.SahtakSubscriptions'  is null.");
		}
	}
}
