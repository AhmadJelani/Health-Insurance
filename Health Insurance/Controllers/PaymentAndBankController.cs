using Health_Insurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rehleti.EmailService;

namespace Health_Insurance.Controllers
{
    public class PaymentAndBankController : Controller
    {
        private readonly ModelContext _context;
        private readonly IEmailSender _emailSender;
        public PaymentAndBankController(ModelContext context,IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
		// GET: SahtakBanks/Create
		// GET: SahtakBanks/Create
		public async Task<IActionResult> Payment(int id)
		{
			ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
			ViewData["FirstName"] = HttpContext.Session.GetString("FirstName");
			ViewData["LastName"] = HttpContext.Session.GetString("LastName");
			HttpContext.Session.SetInt32("SubsID",id);
			ViewData["SubscriptionID"] = HttpContext.Session.GetInt32("SubsID");
			if (HttpContext.Session.GetInt32("UserID") == null)
			{
				return RedirectToAction("SignIn", "Account");
			}
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Payment([Bind("Id,CardNameholder,CardNumber,Status,PaymentDate,UserId,BankId,SubscriptionId,Amount")] SahtakPayment sahtakPayment, string Password, float Amount, string AccountName)
		{
			if (ModelState.IsValid)
			{
				var subscription = await _context.SahtakSubscriptions.Where(x => x.Id == sahtakPayment.SubscriptionId).AsNoTracking().FirstOrDefaultAsync();
				sahtakPayment.UserId = HttpContext.Session.GetInt32("UserID");
				var OneSubs = await _context.UserSubscriptions.Where(x => x.UserId == HttpContext.Session.GetInt32("UserID")).AsNoTracking().FirstOrDefaultAsync();
				if (OneSubs != null && OneSubs.DateTo > DateTime.Now)
				{
					ViewData["ErrorSubs"] = "You Are Subscribed You Can Not Subscripe with another Subscriptions";
					return View(sahtakPayment);
				}
				if (sahtakPayment.Amount >= subscription.Price)
				{
					sahtakPayment.Amount -= subscription.Price;
					sahtakPayment.Status = "Accepted";
					sahtakPayment.PaymentDate = DateTime.Now;

					UserSubscription subs = new UserSubscription();
					subs.UserId = sahtakPayment.UserId;
					subs.SubscriptionId = sahtakPayment.SubscriptionId;
					subs.DateFrom = DateTime.Now;
					subs.PaymentDate = DateTime.Now;

					SahtakBank bank = new SahtakBank();
					bank.Amount = sahtakPayment.Amount;
					bank.AccountName = AccountName;
					bank.Password = Password;
					if (subscription.Period != null)
					{
						int monthsToAdd;
						if (int.TryParse(subscription.Period.ToString(), out monthsToAdd) && monthsToAdd > 0)
						{
							subs.DateTo = DateTime.Now.AddMonths(monthsToAdd);
						}
					}
					_context.SahtakBanks.Add(bank);
					var newPay = new SahtakPayment
					{
						Amount = sahtakPayment.Amount,
						CardNameholder = sahtakPayment.CardNameholder,
						SubscriptionId = subs.SubscriptionId,
						PaymentDate = DateTime.Now,
						UserId = sahtakPayment.UserId,
						CardNumber = sahtakPayment.CardNumber,
						Status = sahtakPayment.Status,
					};
					var newSubs = new UserSubscription
					{
						UserId = sahtakPayment.UserId,
						SubscriptionId = subs.SubscriptionId,
						DateFrom = subs.DateFrom,
						DateTo = subs.DateTo,
						PaymentDate = DateTime.Now
					};
					_context.SahtakBanks.Add(bank);
					_context.SahtakPayments.Add(newPay);
					_context.UserSubscriptions.Add(newSubs);
					await _context.SaveChangesAsync();

					var user = await _context.SahtakUsers.Where(x => x.Id == sahtakPayment.UserId).AsNoTracking().FirstOrDefaultAsync();
					await _emailSender.SendEmailAsync(user.Email, "Confirmation of Subscription and Successful Payment",
						"Dear Valued Client,\r\n\r\nI hope this message finds you well. We would like to express our appreciation for choosing" +
						" Sahtak Health Insurance as your trusted healthcare partner. It is our commitment to provide you with the highest" +
						" standard of service, and we are pleased to inform you that your subscription is now active, with the payment successfully" +
						" processed.\r\n\r\nSubscription Details:\r\n\r\nSubscriber Name: " + user.FirstName + " " + user.LastName + "\r\nPHone Number: " + user.PhoneNumber + "\r\nSubscription" +
						" Name: " + subscription.Name + "\r\nEffective Date: " + subs.DateTo + "\r\nPayment Details:\r\n\r\nPayment" +
						" Date: " + newPay.PaymentDate + "\r\nPayment Amount: " + subscription.Price + "\r\nYour health and well-being" +
						" are of utmost importance to us, and we want to assure you that our dedicated team is here to support you in all your healthcare" +
						" needs. Should you have any questions, require assistance with your policy, or need to file a claim, please do not hesitate to reach" +
						" out to our Customer Support team at [Customer Support Email/Phone].\r\n\r\nFor your convenience, you can access your policy information," +
						" including coverage details and important documents, by logging into your Sahtak Health Insurance account on our website" +
						".\r\n\r\nThank you for entrusting us with your healthcare insurance needs. We look forward to providing you with excellent service and the" +
						" peace of mind that comes with knowing you have comprehensive health coverage.\r\n\r\nIf you have any questions or require further assistance," +
						" please feel free to get in touch. We are here to serve you.\r\n\r\nBest regards,\r\n\r\nClient Services Team\r\nSahtak Insurance" +
						" Insurance");
					return RedirectToAction(nameof(Index), "Home");


				}
				else
				{
					ViewData["ErrorPayment"] = "You Don't Have Enough Amount For This Subscription";
					return View();
				}
			}
			ViewData["BankId"] = new SelectList(_context.SahtakBanks, "Id", "Id", sahtakPayment.BankId);
			ViewData["SubscriptionId"] = new SelectList(_context.SahtakSubscriptions, "Id", "Id", sahtakPayment.SubscriptionId);
			ViewData["UserId"] = new SelectList(_context.SahtakUsers, "Id", "Id", sahtakPayment.UserId);
			return View(sahtakPayment);
		}


	}
}
