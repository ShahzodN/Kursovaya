using Kursovaya.Data;
using Kursovaya.Identity;
using Kursovaya.Models;
using Kursovaya.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kursovaya.Controllers
{
	[Authorize(Roles = "HotelAdmin")]
	public class HotelAdminController : Controller
	{
		private readonly DataContext db;
		private readonly UserManager<Account> userManager;

		public HotelAdminController(DataContext _db, UserManager<Account> _userManager)
		{
			db = _db;
			userManager = _userManager;
		}

		#region GET

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			ViewBag.Title = "Главная";
			var admin = await userManager.GetUserAsync(User);
			ViewData["HotelName"] = admin.Hotel.Name;
			return View();
		}

		[HttpGet]
		public IActionResult CreateEmployee()
		{
			ViewBag.Title = "Добавить сотрудника";
			return View();
		}

		[HttpGet]
		public IActionResult CreateRoom()
		{
			ViewBag.Title = "Добавить комнату";
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetBookings(DateTime date)
		{
			ViewBag.Title = "Бронирования";
			var admin = await userManager.GetUserAsync(User);
			List<BookingHistory> bookings = null;
			if (date == DateTime.MinValue)
				bookings = await db.BookingHistories.Where(bh => bh.Room.HotelId == admin.HotelId && bh.CheckIn.Date == DateTime.Today.Date)
											.ToListAsync();
			else
				bookings = await db.BookingHistories.Where(bh => bh.Room.HotelId == admin.HotelId && bh.CheckIn.Date == date.Date)
											.ToListAsync();

			return View(bookings);
		}

		[HttpGet]
		public async Task<IActionResult> GetVisitorProfile(int historyId)
		{
			var history = await db.BookingHistories.FindAsync(historyId);
			ViewBag.Title = history.Visitor.Account.UserName;
			if (history is not null)
				return View(history);

			return NotFound();
		}

		[HttpGet]
		public IActionResult RegisterVisitor(int id)
		{
			ViewBag.Title = "Регистрация";
			RegisterVisitorViewModel vm = new RegisterVisitorViewModel() { VisitorId = id };
			return View(vm);
		}

		[HttpGet]
		public async Task<IActionResult> Rooms()
		{
			ViewBag.Title = "Номера";
			var rooms = (await userManager.GetUserAsync(User)).Hotel.Rooms;
			return View(rooms);
		}

		[HttpGet]
		public async Task<IActionResult> EditRoom(int id)
		{
			ViewBag.Title = "Редактировать комнату";
			var room = await db.Rooms.FindAsync(id);
			if (room is not null)
			{
				EditRoomViewModel vm = new EditRoomViewModel()
				{
					RoomId = id,
					HotelId = room.HotelId,
					Number = room.Number,
					Price = room.Price,
					Square = room.Square
				};
				return View(vm);
			}
			return NotFound();
		}

		[HttpGet]
		public async Task<IActionResult> GetEmployees()
		{
			ViewBag.Title = "Сотрудники";
			var thisAdmin = await userManager.GetUserAsync(User);
			var employees = thisAdmin.Hotel.Employees;
			return View(employees);
		}

		[HttpGet]
		public async Task<IActionResult> GetEmployee(int id)
		{
			ViewBag.Title = "Сотрудник";
			var employee = await db.Employees.FindAsync(id);
			if (employee is not null)
			{
				var visitInfos = await db.EmployeeVisitInfos.Where(v => v.EmployeeId == employee.Id
																		&& v.CheckIn.Month == DateTime.Today.Month)
															.OrderBy(v=>v.CheckIn.Date)
															.ToListAsync();
				
				EditEmployeeViewModel vm = new EditEmployeeViewModel()
				{
					Id = employee.Id,
					HotelId = employee.HotelId,
					AccountId = employee.AccountId,
					PassportId = employee.PassportId,
					FirstName = employee.FirstName,
					LastName = employee.LastName,
					MiddleName = employee.MiddleName,
					Position = employee.Position,
					Gender = employee.Gender,
					VisitInfos = visitInfos
				};
				return View(vm);
			}
			return NotFound();
		}

		#endregion

		#region POST

		[HttpPost]
		public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var currentAdmin = await userManager.GetUserAsync(User);
				if (db.Employees.Any(e => e.HotelId == currentAdmin.HotelId && e.Passport.SeriaNumber == vm.SeriaNumber)
					|| db.Passports.Any(p => p.SeriaNumber == vm.SeriaNumber))
				{
					ModelState.AddModelError(string.Empty, "Такой сотрудник или паспорт уже существует");
					return View(vm);
				}

				Passport newPassport = new Passport()
				{
					FirstName = vm.FirstName,
					LastName = vm.LastName,
					MiddleName = vm.MiddleName,
					Gender = vm.Gender,
					PlaceOfBirth = vm.PlaceOfBirth,
					PlaceOfResidence = vm.PlaceOfResidence,
					SeriaNumber = vm.SeriaNumber,
					DateOfBirth = vm.DateOfBirth,
					DateOfIssue = vm.DateOfIssue,
					ExpirationDate = vm.ExpirationDate
				};

				Employee newEmployee = new Employee()
				{
					FirstName = vm.FirstName,
					LastName = vm.LastName,
					MiddleName = vm.MiddleName,
					Gender = vm.Gender,
					Position = vm.Position,
					Hotel = currentAdmin.Hotel,
					HotelId = (int)currentAdmin.HotelId,
					Passport = newPassport
				};

				await db.Passports.AddAsync(newPassport);
				await db.Employees.AddAsync(newEmployee);
				await db.SaveChangesAsync();
			}
			return RedirectToAction("GetEmployees");
		}

		[HttpPost]
		public async Task<IActionResult> CreateRoom(CreateRoomViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.GetUserAsync(User);
				if (!db.Rooms.Any(r => r.Number == vm.Number && r.HotelId == user.HotelId))
				{
					var newRoom = new Room()
					{
						Number = vm.Number,
						Square = vm.Square,
						Price = vm.Price,
						IsFamilyRoom = vm.IsFamilyRoom,
						Hotel = user.Hotel
					};
					await db.Rooms.AddAsync(newRoom);
					await db.SaveChangesAsync();
					return RedirectToAction("Rooms");
				}
				else
					ModelState.AddModelError(string.Empty, "Такая комната уже существует");
			}
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> RegisterVisitor(RegisterVisitorViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var visitor = await db.Visitors.FindAsync(vm.VisitorId);
				var passport = new Passport()
				{
					DateOfBirth = vm.DateOfBirth,
					DateOfIssue = vm.DateOfIssue,
					ExpirationDate = vm.ExpirationDate,
					FirstName = vm.FirstName,
					LastName = vm.LastName,
					MiddleName = vm.MiddleName,
					Gender = vm.Gender,
					PlaceOfBirth = vm.PlaceOfBirth,
					PlaceOfResidence = vm.PlaceOfResidence,
					SeriaNumber = vm.SeriaNumber,
					Visitor = visitor
				};
				await db.Passports.AddAsync(passport);
				await db.SaveChangesAsync();
				visitor.Passport = passport;
				return RedirectToAction("GetBookings");
			}
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> EditRoom(EditRoomViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var room = new Room()
				{
					Id = vm.RoomId,
					HotelId = vm.HotelId,
					Square = vm.Square,
					Price = vm.Price,
					Number = vm.Number
				};
				db.Rooms.Update(room);
				await db.SaveChangesAsync();
				return RedirectToAction("Rooms");
			}
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteRoom(int id)
		{
			var room = await db.Rooms.FindAsync(id);
			if (room is not null)
			{
				db.Rooms.Remove(room);
				await db.SaveChangesAsync();
				return RedirectToAction("Rooms");
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> EditEmployee(EditEmployeeViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var employee = new Employee()
				{
					Id = vm.Id,
					HotelId = vm.HotelId,
					PassportId = vm.PassportId,
					AccountId = vm.AccountId,
					FirstName = vm.FirstName,
					LastName = vm.LastName,
					MiddleName = vm.MiddleName,
					Gender = vm.Gender,
					Position = vm.Position
				};
				db.Employees.Update(employee);
				await db.SaveChangesAsync();
				return RedirectToAction("GetEmployees");
			}
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> BlockVisitor(int historyId)
		{
			var history = await db.BookingHistories.FindAsync(historyId);
			if (history is not null)
			{
				var isUnique = await db.Blacklist.AnyAsync(b => b.VisitorId == history.VisitorId && b.CheckIn == history.CheckIn);
				if (!isUnique)
				{
					await db.Blacklist.AddAsync(new Blacklist()
					{
						CheckIn = history.CheckIn,
						CheckOut = history.CheckOut,
						Visitor = history.Visitor,
						Hotel = history.Room.Hotel
					});
					db.BookingHistories.Remove(history);
					await db.SaveChangesAsync();
					return RedirectToAction("GetBookings");
				}
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> UnblockVisitor(int id)
		{
			var record = await db.Blacklist.FindAsync(id);
			if (record is not null)
			{
				db.Blacklist.Remove(record);
				await db.SaveChangesAsync();
				return RedirectToAction("Index", "Statistics");
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> DeleteEmployee(int id)
		{
			int adminId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
			var employee = await db.Employees.FindAsync(id);
			if (employee.AccountId == adminId)
				return RedirectToAction("GetEmployees");
			if (employee is not null)
			{
				db.Employees.Remove(employee);
				await db.SaveChangesAsync();
				return RedirectToAction("GetEmployees");
			}
			return NotFound();
		}
		
		#endregion
	}
}
