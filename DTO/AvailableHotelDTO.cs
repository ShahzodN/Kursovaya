using Kursovaya.Models;
using System.Collections.Generic;

namespace Kursovaya.DTO
{
	public class AvailableHotelDTO
	{
		public AvailableHotelDTO()
		{
			HotelReviews = new List<Review>();
		}
		public string HotelName { get; set; }
		public double HotelRating { get; set; }
		public int RoomId { get; set; }
		public decimal Price { get; set; }
		public double DistanceFromCenter { get; set; }
		public string HotelAddress { get; set; }
		public List<Review> HotelReviews { get; set; }
	}
}
