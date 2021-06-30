using Kursovaya.DTO;
using System;
using System.Collections.Generic;

namespace Kursovaya.ViewModels
{
	public class AvailableHotelsViewModel
	{
		public AvailableHotelsViewModel()
		{
			Hotels = new List<AvailableHotelDTO>();
		}
		public List<AvailableHotelDTO> Hotels { get; set; }
		public string City { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public int MinPrice { get; set; }
		public int MaxPrice { get; set; } = int.MaxValue;
		public bool IsPetFriendly { get; set; }
		public bool HasParkinglot { get; set; }
		public bool IsFamilyRoom { get; set; }
		public bool Breakfast { get; set; }
		public bool HasFreeWiFi { get; set; }
		public bool Filter { get; set; }

		public string GetCheckIn() => CheckIn.ToString("yyyy-MM-ddTHH:mm");
		public string GetCheckOut() => CheckOut.ToString("yyyy-MM-ddTHH:mm");
	}
}
