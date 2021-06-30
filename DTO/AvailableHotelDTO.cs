using Kursovaya.Models;
using System.Collections.Generic;

namespace Kursovaya.DTO
{
	public class AvailableHotelDTO
	{
		public AvailableHotelDTO()
		{
			HotelReviews = new List<Review>();
			Rooms = new List<Room>();
		}
		public string HotelName { get; set; }
		public double HotelRating { get; set; }
		public double DistanceFromCenter { get; set; }
		public string HotelAddress { get; set; }
		public bool IsPetFriendly { get; set; }
		public bool HasParkinglot { get; set; }
		public bool HasBreakfast { get; set; }
		public bool HasFreeWiFi { get; set; }
		public List<Review> HotelReviews { get; set; }
		public List<Room> Rooms { get; set; }
	}
}
