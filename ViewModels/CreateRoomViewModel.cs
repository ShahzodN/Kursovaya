using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class CreateRoomViewModel
	{
		[Display(Name = "Номер комнаты")]
		[Required]
		public short Number { get; set; }

		[Required]
		public double Square { get; set; }

		[Display(Name = "Цена за 12 часов")]
		[Required]
		public decimal Price { get; set; }

		[Display(Name = "Семейный номер")]
		[Required]
		public bool IsFamilyRoom { get; set; }
	}
}
