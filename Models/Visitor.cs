using Kursovaya.Identity;
using System;
using System.Collections.Generic;

namespace Kursovaya.Models
{
	public class Visitor
	{
		public Visitor()
		{
			BookingHistories = new List<BookingHistory>();
		}
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public virtual Account Account { get; set; }
		public int AccountId { get; set; }
		public virtual Passport Passport { get; set; }
		public int? PassportId { get; set; }
		public virtual Blacklist Blacklist { get; set; }
		public virtual List<BookingHistory> BookingHistories { get; set; }
	}
}
