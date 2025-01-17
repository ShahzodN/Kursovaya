﻿using Kursovaya.Data;
using Kursovaya.DTO;
using Kursovaya.EqualityComparer;
using Kursovaya.Identity;
using Kursovaya.Models;
using Kursovaya.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursovaya.Controllers
{
	public class HomeController : Controller
	{
		private readonly DataContext db;
		private readonly UserManager<Account> userManager;
		private readonly RoleManager<IdentityRole<int>> roleManager;
		private readonly IWebHostEnvironment webHostEnvironment;

		public HomeController(UserManager<Account> _userManager, RoleManager<IdentityRole<int>> _roleManager,
			DataContext _db, IWebHostEnvironment _webHostEnvironment)
		{
			db = _db;
			userManager = _userManager;
			roleManager = _roleManager;
			webHostEnvironment = _webHostEnvironment;
		}

		[HttpGet]
		public IActionResult Index()
		{
			ViewBag.Title = "Главная";
			if (User.Identity.IsAuthenticated)
			{
				if (User.IsInRole("AppAdmin"))
					return RedirectToAction("Index", "AppAdmin");
				if (User.IsInRole("HotelAdmin"))
					return RedirectToAction("Index", "HotelAdmin");
			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetCities()
		{
			var cities = await db.Cities.ToListAsync();
			return Json(cities);
		}

		[HttpGet]
		public async Task<IActionResult> AvailableHotels(AvailableHotelsViewModel vm)
		{
			ViewBag.Title = "Результаты поиска";
			if (vm.CheckIn < DateTime.Now) // проверка дата заезда и текущей даты
				ModelState.AddModelError(string.Empty, "Дата заезда должна быть в будущем");
			if (vm.CheckOut < vm.CheckIn) // проверка дата заезда и отъезда
				ModelState.AddModelError(string.Empty, "Дата отъезда не может быть раньше заезда");
			if (ModelState.ErrorCount > 0)
				return View("Index", vm);
			decimal days = Convert.ToDecimal((vm.CheckOut - vm.CheckIn).TotalHours / 24);

			var rooms = await db.Rooms
						.Include(r => r.Hotel)
						.Include(r => r.BookingHistories)
						.Where(r => (r.Hotel.City.Name.ToUpper() == vm.City.Trim().ToUpper())
								&& (!r.BookingHistories.Any(bh => ((bh.CheckOut >= vm.CheckIn && bh.CheckOut <= vm.CheckOut)
															|| (bh.CheckIn >= vm.CheckIn && bh.CheckIn <= vm.CheckOut)))
									|| r.BookingHistories.Count == 0)
								&& (r.Price * days >= vm.MinPrice && r.Price * days <= vm.MaxPrice))
						.ToListAsync();

			if (vm.Filter)
				rooms = FilterRooms(vm, rooms).ToList();

			var groupedRooms = rooms.GroupBy(r => r.HotelId);
			List<AvailableHotelDTO> hotels = new List<AvailableHotelDTO>();

			foreach (var a in groupedRooms)
			{
				var availableHotel = new AvailableHotelDTO();
				foreach (var b in a)
				{
					availableHotel.HotelName = b.Hotel.Name;
					availableHotel.HotelAddress = b.Hotel.Address;
					availableHotel.DistanceFromCenter = b.Hotel.DistanceFromCenter;
					availableHotel.HotelRating = b.Hotel.StarRating;
					availableHotel.HasParkinglot = b.Hotel.HasParkinglot;
					availableHotel.IsPetFriendly = b.Hotel.IsPetFriendly;
					availableHotel.HasFreeWiFi = b.Hotel.HasFreeWiFi;
					availableHotel.HasBreakfast = b.Hotel.HasBreakfast;
					availableHotel.Rooms.Add(b);
				}
				hotels.Add(availableHotel);
			}

			return View(new AvailableHotelsViewModel()
			{
				Hotels = hotels,
				City = vm.City,
				CheckIn = vm.CheckIn,
				CheckOut = vm.CheckOut,
				MaxPrice = vm.MaxPrice,
				MinPrice = vm.MinPrice,
				Breakfast = vm.Breakfast,
				HasFreeWiFi = vm.HasFreeWiFi,
				IsFamilyRoom = vm.IsFamilyRoom,
				HasParkinglot = vm.HasParkinglot,
				IsPetFriendly = vm.IsPetFriendly,
				Filter = vm.Filter
			});
		}

		[HttpPost]
		[Authorize(Roles = "Visitor")]
		public async Task<IActionResult> ReserveRoom(int roomId, DateTime checkIn, DateTime checkOut, decimal price)
		{
			var room = await db.Rooms.FindAsync(roomId);
			if (room is not null && checkIn >= DateTime.Today)
			{
				var newBooking = new BookingHistory()
				{
					CheckIn = checkIn,
					CheckOut = checkOut,
					Price = price,
					Room = room,
					Visitor = (await userManager.GetUserAsync(User)).Visitor
				};
				await db.BookingHistories.AddAsync(newBooking);
				await db.SaveChangesAsync();
				return RedirectToAction("GetHistories", "Visitor");
			}
			return NotFound();
		}

		private List<Room> FilterRooms(AvailableHotelsViewModel vm, List<Room> rooms)
		{
			var hours = Convert.ToDecimal((vm.CheckOut - vm.CheckIn).TotalHours / 24);

			if (vm.IsFamilyRoom)
				rooms = rooms.Where(r => r.IsFamilyRoom == vm.IsFamilyRoom).ToList();
			if (vm.HasParkinglot)
				rooms = rooms.Where(r => r.Hotel.HasParkinglot == vm.HasParkinglot).ToList();
			if (vm.HasFreeWiFi)
				rooms = rooms.Where(r => r.Hotel.HasFreeWiFi == vm.HasFreeWiFi).ToList();
			if (vm.IsPetFriendly)
				rooms = rooms.Where(r => r.Hotel.IsPetFriendly == vm.IsPetFriendly).ToList();
			if (vm.Breakfast)
				rooms = rooms.Where(r => r.Hotel.HasBreakfast == vm.Breakfast).ToList();
			return rooms;
		}
	}
}
