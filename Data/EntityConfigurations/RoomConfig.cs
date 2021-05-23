using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class RoomConfig : IEntityTypeConfiguration<Room>
	{
		public void Configure(EntityTypeBuilder<Room> builder)
		{
			builder.ToTable("Rooms");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Number)
				.IsRequired();

			builder.Property(e => e.Square)
				.IsRequired();

			builder.Property(e => e.Price)
				.IsRequired();

			builder.Property(e => e.IsFamilyRoom)
				.IsRequired();

			builder.HasOne(e => e.Hotel)
				.WithMany(e => e.Rooms)
				.HasForeignKey(e => e.HotelId);
		}
	}
}
