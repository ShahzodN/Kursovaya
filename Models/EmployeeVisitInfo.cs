using System;

namespace Kursovaya.Models
{
	public class EmployeeVisitInfo
	{
		public long Id { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public virtual Employee Employee { get; set; }
		public int EmployeeId { get; set; }
	}
}
