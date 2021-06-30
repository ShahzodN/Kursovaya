using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class CreateRoomViewModel
	{
		[Display(Name = "Номер комнаты")]
		[Required(ErrorMessage = "Укажите номер")]
		public short Number { get; set; }

		[Required(ErrorMessage = "Укажите площадь")]
		public double Square { get; set; }

		[Display(Name = "Цена за сутки")]
		[Required(ErrorMessage = "Укажите цену")]
		public decimal Price { get; set; }

		[Display(Name = "Семейный номер")]
		[Required]
		public bool IsFamilyRoom { get; set; }

		[Required(ErrorMessage = "Выберите тип томера")]
		public RoomTypeEnum Type { get; set; }
	}
}
