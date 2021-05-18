using Kursovaya.Data;
using Kursovaya.Identity;
using Kursovaya.Models;
using Kursovaya.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kursovaya.Controllers
{
	[Authorize(Roles = "AppAdmin")]
	public class AppAdminController : Controller
	{
		private readonly DataContext db;
		private readonly UserManager<Account> userManager;
		private readonly RoleManager<IdentityRole<int>> roleManager;
		private readonly IWebHostEnvironment webHostEnvironment;

		public AppAdminController(UserManager<Account> _userManager, DataContext _db,
			IWebHostEnvironment _webHostEnvironment, RoleManager<IdentityRole<int>> _roleManager)
		{
			db = _db;
			userManager = _userManager;
			roleManager = _roleManager;
			webHostEnvironment = _webHostEnvironment;
		}

		#region GET

		[HttpGet]
		public IActionResult Index()
		{
			ViewBag.Title = "Главная";
			return View();
		}

		[HttpGet]
		public IActionResult CreateHotelAdmin()
		{
			ViewBag.Title = "Добавить администратора";
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CreateHotel()
		{
			ViewBag.Title = "Добавить гостиницу";
			var cities = await db.Cities.ToArrayAsync();
			ViewBag.Cities = new SelectList(cities, "Id", "Name");
			return View();
		}

		[HttpGet]
		public IActionResult CreateCity()
		{
			ViewBag.Title = "Добавить город";
			return View();
		}

		#endregion

		#region POST

		[HttpPost]
		public async Task<IActionResult> CreateHotelAdmin(CreateHotelAdminViewModel vm)
		{
			if (ModelState.IsValid)
			{
				Hotel hotel = await db.Hotels.Where(h => h.Name.ToUpper() == vm.HotelName.ToUpper()).FirstOrDefaultAsync();
				if (hotel is not null)
				{
					var account = new Account() { UserName = vm.UserName, Hotel = hotel, Email = vm.Email };
					var createStatus = await userManager.CreateAsync(account, vm.Password);
					if (createStatus.Succeeded)
					{
						await userManager.AddToRoleAsync(account, "HotelAdmin");
						var emp = new Employee()
						{
							FirstName = vm.FirstName,
							LastName = vm.LastName,
							MiddleName = vm.MiddleName,
							Gender = vm.Gender,
							Position = vm.Position,
							AccountId = account.Id,
							HotelId = hotel.Id
						};
						await db.Employees.AddAsync(emp);
						await db.SaveChangesAsync();
						return RedirectToAction("index");
					}
					else
						ModelState.AddModelError(string.Empty, "Ошибка");
				}
				else
					ModelState.AddModelError(string.Empty, $"Не найдено гостиница с названием {vm.HotelName}");
			}
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> CreateHotel(CreateHotelViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var newHotel = new Hotel()
				{
					Name = vm.Name,
					Address = vm.Address,
					Email = vm.Email,
					PhoneNumber = vm.PhoneNumber,
					DistanceFromCenter = vm.DistanceFromCenter,
					StarRating = 0,
					HasFreeWiFi = vm.HasFreeWiFi,
					IsPetFriendly = vm.IsPetFriendly,
					HasParkinglot = vm.HasParkingLot,
					HasBreakfast = vm.HasBreakfast,
					CityId = vm.CityId
				};
				await db.Hotels.AddAsync(newHotel);
				await db.SaveChangesAsync();
				return View("Index");
			}
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCity(CreateCityViewModel vm)
		{
			if (ModelState.IsValid)
			{
				await db.Cities.AddAsync(new City() { Name = vm.Name });
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(vm);
		}

		#endregion
	}
}
