using System;

namespace Kursovaya.Models
{
	public class BookingHistory
	{
		public int Id { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public decimal Price { get; set; }
		public virtual Visitor Visitor { get; set; }
		public int VisitorId { get; set; }
		public virtual Room Room { get; set; }
		public int RoomId { get; set; }
		public virtual Review Review { get; set; }
		public int? ReviewId { get; set; }
	}
}
