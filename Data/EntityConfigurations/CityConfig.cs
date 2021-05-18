using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class CityConfig : IEntityTypeConfiguration<City>
	{
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder.ToTable("Cities");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(40);
		}
	}
}
