using System;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class CreateEmployeeViewModel
	{
		[Display(Name = "Имя")]
		[Required(ErrorMessage = "Введите имя")]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Display(Name = "Фамилия")]
		[Required(ErrorMessage = "Введите фамилию")]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Display(Name = "Отчество")]
		public string MiddleName { get; set; }

		[Display(Name = "Должность")]
		[Required(ErrorMessage = "Выберите должность")]
		public PositionEnum Position { get; set; }

		[Display(Name = "Пол")]
		[Required(ErrorMessage = "Выберите пол")]
		public GenderEnum Gender { get; set; }

		[Display(Name = "Серия паспорта")]
		[Required(ErrorMessage = "Введите номер и серию паспорта")]
		public string SeriaNumber { get; set; }

		[Display(Name = "Место рождения")]
		[Required(ErrorMessage = "Введите место рождения")]
		public string PlaceOfBirth { get; set; }

		[Display(Name = "Место проживание")]
		[Required(ErrorMessage = "Введите место проживания")]
		public string PlaceOfResidence { get; set; }

		[Display(Name = "Дата рождения")]
		public DateTime DateOfBirth { get; set; }

		[Display(Name = "Дата выдачи")]
		public DateTime DateOfIssue { get; set; }

		[Display(Name = "Дата окончания срока")]
		public DateTime ExpirationDate { get; set; }
	}
}
