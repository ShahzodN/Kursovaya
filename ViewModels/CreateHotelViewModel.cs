using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class CreateHotelViewModel
	{
		[Display(Name = "Имя гостиницы")]
		[Required]
		[MaxLength(60)]
		public string Name { get; set; }

		[Display(Name = "Адрес")]
		[Required]
		public string Address { get; set; }

		[Display(Name = "Адрес электронной почты")]
		[DataType(DataType.EmailAddress)]
		[Required]
		[MaxLength(60)]
		public string Email { get; set; }

		[Display(Name = "Номер телефона")]
		[MaxLength(20, ErrorMessage = "Номер телефона не может содержат больше чем 20 символов")]
		[DataType(DataType.PhoneNumber)]
		[Required]
		public string PhoneNumber { get; set; }

		[Display(Name = "Расстояние от центра города")]
		[Required]
		public double DistanceFromCenter { get; set; }

		[Required]
		public int CityId { get; set; }

		[Display(Name = "Можно с питомцами")]
		public bool IsPetFriendly { get; set; }
		
		[Display(Name = "Парковка")]
		public bool HasParkingLot { get; set; }

		[Display(Name = "Завтрак")]
		public bool HasBreakfast { get; set; }
		
		[Display(Name = "Беспплатный Wi-Fi")]
		public bool HasFreeWiFi { get; set; }

		[Required]
		public List<IFormFile> Files { get; set; }
	}
}
