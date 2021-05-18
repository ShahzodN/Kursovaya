using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class BookingHistoryConfig : IEntityTypeConfiguration<BookingHistory>
	{
		public void Configure(EntityTypeBuilder<BookingHistory> builder)
		{
			builder.ToTable("BookingHistories");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.CheckIn)
				.IsRequired();

			builder.Property(e => e.CheckOut);

			builder.Property(e => e.Price)
				.IsRequired();

			builder.HasOne(e => e.Visitor)
				.WithMany(e => e.BookingHistories)
				.HasForeignKey(e => e.VisitorId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(e => e.Room)
				.WithMany(e => e.BookingHistories)
				.HasForeignKey(e => e.RoomId);
		}
	}
}
