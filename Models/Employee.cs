using Kursovaya.Identity;
using System.Collections.Generic;

namespace Kursovaya.Models
{
	public class Employee
	{
		public Employee()
		{
			VisitInfos = new List<EmployeeVisitInfo>();
		}
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public PositionEnum Position { get; set; }
		public GenderEnum Gender { get; set; }
		public virtual Account Account { get; set; }
		public int? AccountId { get; set; }
		public virtual Hotel Hotel { get; set; }
		public int HotelId { get; set; }
		public virtual Passport Passport { get; set; }
		public int? PassportId { get; set; }
		public virtual List<EmployeeVisitInfo> VisitInfos { get; set; }
	}
}
