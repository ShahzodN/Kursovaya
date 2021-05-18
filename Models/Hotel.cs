using Kursovaya.Identity;
using System.Collections.Generic;

namespace Kursovaya.Models
{
	public class Hotel
	{
		public Hotel()
		{
			Employees = new List<Employee>();
			Rooms = new List<Room>();
			AppUsers = new List<Account>();
			Blacklist = new List<Blacklist>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public double StarRating { get; set; }
		public double DistanceFromCenter { get; set; }
		public bool IsPetFriendly { get; set; }
		public bool HasParkinglot { get; set; }
		public bool HasBreakfast { get; set; }
		public bool HasFreeWiFi { get; set; }
		public virtual City City { get; set; }
		public int CityId { get; set; }
		public virtual List<Employee> Employees { get; set; }
		public virtual List<Room> Rooms { get; set; }
		public virtual List<Account> AppUsers { get; set; }
		public virtual List<Blacklist> Blacklist { get; set; }
	}
}
