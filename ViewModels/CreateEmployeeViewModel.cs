using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursovaya.ViewModels
{
	public class CreateEmployeeViewModel
	{
		[Display(Name = "Имя")]
		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Display(Name = "Фамилия")]
		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[Display(Name = "Отчество")]
		public string MiddleName { get; set; }

		[Display(Name = "Должность")]
		[Required]
		[MaxLength(50)]
		public PositionEnum Position { get; set; }

		[Display(Name = "Пол")]
		[Required]
		public GenderEnum Gender { get; set; }

		[Display(Name = "Серия паспорта")]
		[Required]
		public string SeriaNumber { get; set; }

		[Display(Name = "Место рождения")]
		[Required]
		public string PlaceOfBirth { get; set; }

		[Display(Name = "Место проживание")]
		[Required]
		public string PlaceOfResidence { get; set; }

		[Display(Name = "Дата рождения")]
		[Required]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		[Display(Name = "Дата выдачи")]
		[Required]
		[DataType(DataType.Date)]
		public DateTime DateOfIssue { get; set; }

		[Display(Name = "Дата окончания срока")]
		[Required]
		[DataType(DataType.Date)]
		public DateTime ExpirationDate { get; set; }
	}
}
