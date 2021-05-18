using Kursovaya.Models;
using System;
using System.Collections.Generic;

namespace Kursovaya.EqualityComparer
{
	public class RoomComparer : IEqualityComparer<Room>
	{
		public bool Equals(Room x, Room y)
		{
			if (Object.ReferenceEquals(x, y))
				return true;
			if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
				return false;
			return x.HotelId == y.HotelId;
		}

		public int GetHashCode(Room room)
		{
			if (Object.ReferenceEquals(room, null)) return 0;

			int hashHotelId = room.HotelId == 0 ? 0 : room.HotelId.GetHashCode();

			return hashHotelId;
		}
	}
}
