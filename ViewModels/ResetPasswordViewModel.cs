using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		[Required]
		[MinLength(8)]
		public string NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Required]
		[Compare("NewPassword")]
		public string NewPasswordConfirm { get; set; }

		public string Token { get; set; }
	}
}
