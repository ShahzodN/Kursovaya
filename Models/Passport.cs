using System;

namespace Kursovaya.Models
{
	public class Passport
	{
		public int Id { get; set; }
		public string SeriaNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public GenderEnum Gender { get; set; }
		public string PlaceOfBirth { get; set; }
		public string PlaceOfResidence { get; set; }
		public DateTime DateOfIssue { get; set; }
		public DateTime ExpirationDate { get; set; }
		public virtual Visitor Visitor { get; set; }
		public virtual Employee Employee { get; set; }
	}
}
