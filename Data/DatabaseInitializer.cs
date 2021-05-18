using Kursovaya.Identity;
using Kursovaya.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kursovaya.Data
{
	public class DatabaseInitializer
	{
		public static void RoleInit(UserManager<Account> userManager, RoleManager<IdentityRole<int>> roleManager,
			DataContext db)
		{
			if (roleManager.FindByNameAsync("AppAdmin").GetAwaiter().GetResult() is null)
				roleManager.CreateAsync(new IdentityRole<int>("AppAdmin")).GetAwaiter().GetResult();
			if (roleManager.FindByNameAsync("HotelAdmin").GetAwaiter().GetResult() is null)
				roleManager.CreateAsync(new IdentityRole<int>("HotelAdmin")).GetAwaiter().GetResult();
			if (roleManager.FindByNameAsync("Visitor").GetAwaiter().GetResult() is null)
				roleManager.CreateAsync(new IdentityRole<int>("Visitor")).GetAwaiter().GetResult();
			AppAdminInit(userManager, roleManager, db);
			HotelInit(db);
		}

		public static void AppAdminInit(UserManager<Account> userManager, RoleManager<IdentityRole<int>> roleManager,
			DataContext db)
		{
			if (userManager.FindByNameAsync("appAdmin").GetAwaiter().GetResult() is null)
			{
				var user = new Account() { UserName = "appAdmin" };
				var res = userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
				if (res.Succeeded)
				{
					userManager.AddToRoleAsync(user, "AppAdmin").GetAwaiter().GetResult();
					var appAdmin = new AppAdmin() { FirstName = "admin", LastName = "admin", AccountId = user.Id };
					db.AppAdmins.Add(appAdmin);
					db.SaveChanges();
				}
			}
		}

		private static void HotelInit(DataContext db)
		{
			Random rnd = new Random();
			City city = null;
			if (db.Cities.Where(c => c.Name == "Казань").FirstOrDefault() == null)
			{
				city = new City() { Name = "Казань" };
				for (int i = 0; i < 3; i++)
				{
					var hotel = new Hotel()
					{
						Address = $"address{i}",
						City = city,
						DistanceFromCenter = 2.4,
						Email = $"hotel{i}@gmail.com",
						Name = $"hotel{i}",
						PhoneNumber = "+00000000000",
						StarRating = 0,
						HasFreeWiFi = Convert.ToBoolean(rnd.Next(0, 2)),
						HasParkinglot = Convert.ToBoolean(rnd.Next(0, 2)),
						IsPetFriendly = Convert.ToBoolean(rnd.Next(0, 2)),
						HasBreakfast = Convert.ToBoolean(rnd.Next(0, 2))
					};
					db.Hotels.Add(hotel);
				}
				db.Cities.Add(city);
				db.SaveChanges();
			}
		}
	}
}
