using Kursovaya.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class EditEmployeeViewModel
	{
		public EditEmployeeViewModel()
		{
			VisitInfos = new List<EmployeeVisitInfo>();
		}
		public int Id { get; set; }
		public int HotelId { get; set; }
		public int? AccountId { get; set; }
		public int? PassportId { get; set; }

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

		public List<EmployeeVisitInfo> VisitInfos { get; set; }
	}
}
