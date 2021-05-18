namespace Kursovaya.Models
{
	public class Review
	{
		public long Id { get; set; }
		public int Mark { get; set; }
		public string Comment { get; set; }
		public virtual BookingHistory BookingHistory { get; set; }
		public int HistoryId { get; set; }
	}
}
