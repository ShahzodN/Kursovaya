using Kursovaya.Models;
using System.Collections.Generic;

namespace Kursovaya.ViewModels
{
	public class StatisticsViewModel
	{
		public StatisticsViewModel()
		{
			Blacklist = new List<Blacklist>();
		}
		public int HotelRank { get; set; }
		public double HotelRating { get; set; }
		public List<Blacklist> Blacklist { get; set; }
	}
}
