using Kursovaya.Data;
using Kursovaya.Identity;
using Kursovaya.Models;
using Kursovaya.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kursovaya.Controllers
{
	[Authorize(Roles = "Visitor")]
	public class VisitorController : Controller
	{
		private readonly DataContext db;
		private readonly UserManager<Account> userManager;

		public VisitorController(DataContext _db, UserManager<Account> _userManager)
		{
			db = _db;
			userManager = _userManager;
		}

		#region GET

		[HttpGet]
		public IActionResult Index()
		{
			ViewBag.Title = "Главная";
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetHistories()
		{
			ViewBag.Title = "История бронирования";
			var visitor = (await userManager.GetUserAsync(User)).Visitor;
			var histories = await db.BookingHistories
									.Include(bh => bh.Room.Hotel)
									.Where(bh => bh.VisitorId == visitor.Id)
									.AsNoTracking()
									.ToListAsync();
			return View(histories);
		}

		[HttpGet]
		public async Task<IActionResult> Review(int historyId)
		{
			ViewBag.Title = "Оставить отзыв";
			Review visitorReview = null;

			var visitor = (await userManager.GetUserAsync(User)).Visitor;
			var history = await db.BookingHistories.FindAsync(historyId);

			if (history is not null)
				visitorReview = await db.Reviews.FirstOrDefaultAsync(r => r.HistoryId == historyId);

			if (visitorReview is not null)
				return View(new ReviewViewModel() { Comment = visitorReview.Comment, Mark = visitorReview.Mark });

			return View(new ReviewViewModel() { HistoryId = history.Id });
		}

		[HttpGet]
		public IActionResult ChangeInfo()
		{
			ViewBag.Title = "Изменить данные";
			return View();
		}
		#endregion

		#region POST

		[HttpPost]
		public async Task<IActionResult> Review(ReviewViewModel vm)
		{
			if (ModelState.IsValid)
			{
				if (vm is not null)
				{
					await db.Reviews.AddAsync(new Review()
					{
						Comment = vm.Comment,
						Mark = vm.Mark,
						HistoryId = vm.HistoryId
					});
					await db.SaveChangesAsync();
					await CalculateHotelRating(vm.HistoryId);
					return RedirectToAction("GetHistories");
				}
				return BadRequest();
			}
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> ChangeInfo(ChangeInfoViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var account = await userManager.GetUserAsync(User);
				var passwordValidator = HttpContext.RequestServices.GetService(typeof(IPasswordValidator<Account>)) as IPasswordValidator<Account>;
				var passwordHasher = HttpContext.RequestServices.GetService(typeof(IPasswordHasher<Account>)) as IPasswordHasher<Account>;
				var result = await passwordValidator.ValidateAsync(userManager, account, vm.NewPassword);
				if (result.Succeeded)
				{
					account.PasswordHash = passwordHasher.HashPassword(account, vm.NewPassword);
					await userManager.UpdateAsync(account);
					return RedirectToAction("Index", "Home");
				}
			}
			return View(vm);
		}

		[HttpPost]
		private async Task CalculateHotelRating(int historyId)
		{
			var history = await db.BookingHistories.FirstOrDefaultAsync(h=>h.Id == historyId);
			var marks = await db.Reviews.Where(r => r.BookingHistory.Room.HotelId == history.Room.HotelId)
										.Select(r => r.Mark)
										.ToListAsync();
			history.Room.Hotel.StarRating = marks.Count == 0 ? 0 : marks.Sum() / (double)marks.Count;
			await db.SaveChangesAsync();
		}

		[HttpPost]
		public async Task<IActionResult> CancelBooking(int id)
		{
			var history = await db.BookingHistories.FindAsync(id);
			if (history is not null)
			{
				db.BookingHistories.Remove(history);
				await db.SaveChangesAsync();
				return RedirectToAction("GetHistories");
			}
			return NotFound();
		}
		#endregion
	}
}
