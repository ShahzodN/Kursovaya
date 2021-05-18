using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class BlacklistConfig : IEntityTypeConfiguration<Blacklist>
	{
		public void Configure(EntityTypeBuilder<Blacklist> builder)
		{
			builder.ToTable("Blacklist");

			builder.HasKey(z => z.Id);

			builder.Property(z => z.CheckIn);
			
			builder.Property(z => z.CheckOut);

			builder.HasOne(z => z.Visitor)
				.WithOne(v => v.Blacklist)
				.HasForeignKey<Blacklist>(z => z.VisitorId);

			builder.HasOne(z => z.Hotel)
				.WithMany(v => v.Blacklist)
				.HasForeignKey(z => z.HotelId);
		}
	}
}
