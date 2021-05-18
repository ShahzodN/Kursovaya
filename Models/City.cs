using System.Collections.Generic;

namespace Kursovaya.Models
{
	public class City
	{
		public City()
		{
			Hotels = new List<Hotel>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual List<Hotel> Hotels { get; set; }
	}
}
