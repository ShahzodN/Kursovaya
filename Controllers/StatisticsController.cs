using Kursovaya.Data;
using Kursovaya.Identity;
using Kursovaya.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Kursovaya.Controllers
{
	[Authorize(Roles = "HotelAdmin")]
	public class StatisticsController : Controller
	{
		private readonly UserManager<Account> userManager;
		private readonly DataContext db;

		public StatisticsController(UserManager<Account> _userManager, DataContext _db)
		{
			userManager = _userManager;
			db = _db;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			ViewBag.Title = "Статистика";
			var admin = await userManager.GetUserAsync(User);

			var hotelsRating = await db.Hotels.Where(h => h.CityId == admin.Hotel.CityId)
											  .Select(h => h.StarRating)
											  .ToListAsync();

			hotelsRating = hotelsRating.Distinct().ToList();
			var blacklist = await db.Blacklist.Where(b => b.HotelId == admin.HotelId).ToListAsync();
			
			var vm = new StatisticsViewModel()
			{
				HotelRank = hotelsRating.IndexOf(admin.Hotel.StarRating) + 1,
				HotelRating = admin.Hotel.StarRating,
				Blacklist = blacklist
			};
			
			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> GetRatingData()
		{
			var admin = await userManager.GetUserAsync(User);
			var marks = await db.Reviews.Where(r => r.BookingHistory.Room.HotelId == admin.HotelId)
										.Select(r => r.Mark)
										.ToListAsync();
			dynamic[][] marksArr = new dynamic[5][];
			int max = 0;
			for (int i = 0; i < 5; i++)
			{
				marksArr[i] = new dynamic[2];
				marksArr[i][0] = (5 - i).ToString();
				marksArr[i][1] = marks.Count(m => m == 5 - i);
				max = marksArr[i][1] > max ? marksArr[i][1] : max;
			}
			return Json(new { data = marksArr, max = max });
		}

		[HttpGet]
		public async Task<IActionResult> GetVisitorsCount()
		{
			var admin = await userManager.GetUserAsync(User);

			CultureInfo.CurrentCulture = new CultureInfo("ru");

			var months = DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames;

			dynamic[][] data = new dynamic[12][];
			var history = await db.BookingHistories.Where(bh => bh.Room.HotelId == admin.HotelId).ToArrayAsync();
			int max = 0;
			for (int i = 0; i < 12; i++)
			{
				data[i] = new dynamic[2];
				data[i][0] = months[i].ToUpperInvariant();
				data[i][1] = history.Where(h => h.CheckIn.Month == i + 1).Count();
				max = data[i][1] > max ? data[i][1] : max;
			}
			return Json(new { data = data, max = max });
		}

		[HttpGet]
		public async Task<IActionResult> GetVisCountByInterval(DateTime from, DateTime to)
		{
			int newVisitorsCount = 0;
			int oldVisitorsCount = 0;

			var admin = await userManager.GetUserAsync(User);
			var bookings = await db.BookingHistories.Where(bh => bh.Room.HotelId == admin.HotelId
															&& bh.CheckIn.Date >= from
															&& bh.CheckOut.Date <= to).ToListAsync();

			var accountsIdInInterval = bookings.Select(b => b.Visitor.AccountId).Distinct().ToArray();

			var olderBookingsAccountId = await db.BookingHistories.Where(bh => bh.Room.HotelId == admin.HotelId
																				&& bh.CheckIn.Date < from.Date)
																  .Select(bh => bh.Visitor.AccountId)
																  .ToArrayAsync();

			newVisitorsCount = accountsIdInInterval.Except(olderBookingsAccountId).Count();
			oldVisitorsCount = olderBookingsAccountId.Except(accountsIdInInterval).Count();
			return Json(new { totalCount = bookings.Count, newVisitorsCount = newVisitorsCount, oldVisitorsCount = oldVisitorsCount });
		}

		[HttpGet]
		public async Task<IActionResult> GetVisitorsByRegion()
		{
			int i = 0;

			var admin = await userManager.GetUserAsync(User);
			var places = await db.BookingHistories.Where(bh => bh.Room.HotelId == admin.HotelId && bh.Visitor.Passport != null)
													.ToListAsync();

			var groupedPlaces = places.GroupBy(p => p.Visitor.Passport.PlaceOfResidence);
			int placesCount = groupedPlaces.Count();
			dynamic[][] chartData = new dynamic[placesCount][];

			foreach (var p in groupedPlaces)
			{
				chartData[i] = new dynamic[2];
				chartData[i][0] = p.Key;
				chartData[i][1] = p.Count();
				i++;
			}
			return Json(chartData);
		}
	}
}
