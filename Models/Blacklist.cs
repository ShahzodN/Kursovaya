using System;

namespace Kursovaya.Models
{
	public class Blacklist
	{
		public int Id { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public virtual Visitor Visitor { get; set; }
		public int VisitorId { get; set; }
		public virtual Hotel Hotel { get; set; }
		public int HotelId { get; set; }
	}
}
