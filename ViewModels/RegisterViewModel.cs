using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class RegisterViewModel
	{
		[Display(Name = "Имя пользователя")]
		[Required(ErrorMessage = "Введите имя пользователя")]
		public string UserName { get; set; }

		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Введите пароль")]
		[MinLength(5, ErrorMessage = "Длина пароли должна быть больше 5 символов")]
		public string Password { get; set; }

		[Display(Name = "Подтверждение пароля")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string PasswordConfirm { get; set; }

		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Некорректный электронный адрес")]
		[Required(ErrorMessage = "Введите Email")]
		public string Email { get; set; }

		[Display(Name = "Телефон")]
		[DataType(DataType.PhoneNumber, ErrorMessage = "Некорректный номер телефона")]
		[Required(ErrorMessage = "Введите телефон")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Имя")]
		[Required(ErrorMessage = "Введите имя")]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Display(Name = "Фамилия")]
		[Required(ErrorMessage = "Введите фамилию")]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Display(Name = "Отчество")]
		[MaxLength(50)]
		public string MiddleName { get; set; }
	}
}
