using System;
using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class ReviewViewModel
	{
		[Required]
		public int HistoryId { get; set; }

		[Range(1, 5, ErrorMessage = "Ошибка! Нет оценки.")]
		public int Mark { get; set; }

		public string Comment { get; set; }
	}
}
