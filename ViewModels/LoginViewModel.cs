using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Имя пользователя")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
