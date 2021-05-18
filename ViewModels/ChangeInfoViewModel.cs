using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class ChangeInfoViewModel
	{
		[DataType(DataType.Password)]
		[Required]
		[MinLength(8, ErrorMessage = "Пароль должен содержать минимум 8 символов")]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Required]
		[Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
		public string NewPasswordConfirm { get; set; }
	}
}
