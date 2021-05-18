using Kursovaya.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Kursovaya.Identity
{
	public class Account : IdentityUser<int>
	{
		public virtual Employee Employee { get; set; }
		public virtual Visitor Visitor { get; set; }
		public virtual AppAdmin AppAdmin { get; set; }
		public virtual Hotel Hotel { get; set; }
		public DateTime RegisteredDate { get; set; }
		public int? HotelId { get; set; }
	}
}
