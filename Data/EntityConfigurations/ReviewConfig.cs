using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class ReviewConfig : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.ToTable("Reviews");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Mark)
				.IsRequired();

			builder.Property(e => e.Comment);

			builder.HasOne(e => e.BookingHistory)
				.WithOne(e => e.Review)
				.HasForeignKey<Review>(e => e.HistoryId);
		}
	}
}
