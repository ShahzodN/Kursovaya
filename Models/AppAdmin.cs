using Kursovaya.Identity;

namespace Kursovaya.Models
{
	public class AppAdmin
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public virtual Account Account { get; set; }
		public int AccountId { get; set; }
	}
}
