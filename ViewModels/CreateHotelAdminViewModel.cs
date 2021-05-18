using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class CreateHotelAdminViewModel
	{
		[Display(Name = "Имя пользователя")]
		[Required]
		public string UserName { get; set; }

		[Display(Name = "Адрес электронной почты")]
		[DataType(DataType.EmailAddress)]
		[Required]
		public string Email { get; set; }

		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; }

		[Display(Name = "Подтверждение пароля")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		public string PasswordConfirm { get; set; }

		[Display(Name = "Имя")]
		[Required]
		public string FirstName { get; set; }

		[Display(Name = "Фамилия")]
		[Required]
		public string LastName { get; set; }

		[Display(Name = "Отчество")]
		public string MiddleName { get; set; }

		[Display(Name = "Должность")]
		[Required]
		public PositionEnum Position { get; set; }

		[Display(Name = "Пол")]
		[Required]
		public GenderEnum Gender { get; set; }

		[Display(Name = "Название гостиницы")]
		[Required]
		public string HotelName { get; set; }
	}
}
