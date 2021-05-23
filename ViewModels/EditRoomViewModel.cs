using System.ComponentModel.DataAnnotations;

namespace Kursovaya.ViewModels
{
	public class EditRoomViewModel
	{
		public int RoomId { get; set; }
		public int HotelId { get; set; }

		[Display(Name = "Номер комнаты")]
		[Required]
		public short Number { get; set; }

		[Required]
		public double Square { get; set; }

		[Display(Name = "Цена за 12 часов")]
		[Required]
		public decimal Price { get; set; }

		[Required]
		public bool IsFamilyRoom { get; set; }
		
		[Required]
		public RoomTypeEnum Type { get; set; }
	}
}
