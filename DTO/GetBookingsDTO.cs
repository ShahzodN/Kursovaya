using System;

namespace Kursovaya.DTO
{
	public class GetBookingsDTO
	{
		public int Id { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public int RoomNumber { get; set; }
		public decimal Price { get; set; }
		public string UserName { get; set; }
		public bool IsBlockedVisitor { get; set; }
		public bool IsRegistered { get; set; }
	}
}
