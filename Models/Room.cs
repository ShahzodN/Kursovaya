﻿using System.Collections.Generic;

namespace Kursovaya.Models
{
	public class Room
	{
		public Room()
		{
			BookingHistories = new List<BookingHistory>();
		}
		public int Id { get; set; }
		public short Number { get; set; }
		public double Square { get; set; }
		public decimal Price { get; set; }
		public bool IsFamilyRoom { get; set; }
		public RoomTypeEnum Type { get; set; }
		public virtual Hotel Hotel { get; set; }
		public int HotelId { get; set; }
		public virtual List<BookingHistory> BookingHistories { get; set; }
	}
}
