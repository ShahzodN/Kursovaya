using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class RegisterViewModel
	{
		[Display(Name = "Имя пользователя")]
		[Required(ErrorMessage = "Имя пользователя не может быт пустым")]
		public string UserName { get; set; }

		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Пароль не может быт пустым")]
		[MinLength(5, ErrorMessage = "Длина пароли должна быть больше 5 символов")]
		public string Password { get; set; }

		[Display(Name = "Подтверждение пароля")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string PasswordConfirm { get; set; }

		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Email не может быт пустым")]
		public string Email { get; set; }

		[Display(Name = "Имя")]
		[Required(ErrorMessage = "Имя не может быт пустым")]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Display(Name = "Фамилия")]
		[Required(ErrorMessage = "Фамилия не может быт пустой")]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Display(Name = "Отчество")]
		[MaxLength(50)]
		public string MiddleName { get; set; }



	}
}
