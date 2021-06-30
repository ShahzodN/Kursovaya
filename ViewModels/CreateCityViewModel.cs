using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class CreateCityViewModel
	{
		[Display(Name = "Название города")]
		[Required(ErrorMessage = "Введите название города")]
		[MaxLength(40)]
		public string Name { get; set; }
	}
}
