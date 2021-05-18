using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data
{
	public class HotelConfig : IEntityTypeConfiguration<Hotel>
	{
		public void Configure(EntityTypeBuilder<Hotel> builder)
		{
			builder.ToTable("Hotels");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(60);

			builder.Property(e => e.Address)
				.IsRequired();

			builder.Property(e => e.Email)
				.IsRequired()
				.HasMaxLength(60);

			builder.Property(e => e.PhoneNumber)
				.IsRequired()
				.HasMaxLength(20);

			builder.Property(e => e.StarRating)
				.IsRequired();

			builder.Property(e => e.DistanceFromCenter)
				.IsRequired();

			builder.Property(e => e.IsPetFriendly)
				.IsRequired();

			builder.Property(e => e.HasParkinglot)
				.IsRequired();

			builder.Property(e => e.HasFreeWiFi)
				.IsRequired();

			builder.Property(e => e.HasBreakfast)
				.IsRequired();


			builder.HasOne(e => e.City)
				.WithMany(e => e.Hotels)
				.HasForeignKey(e => e.CityId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(e => e.AppUsers)
				.WithOne(e => e.Hotel)
				.HasForeignKey(e => e.HotelId);
		}
	}
}
