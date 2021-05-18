using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class PassportConfig : IEntityTypeConfiguration<Passport>
	{
		public void Configure(EntityTypeBuilder<Passport> builder)
		{
			builder.ToTable("Passports");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.LastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.MiddleName)
				.HasMaxLength(50);

			builder.Property(e => e.DateOfBirth)
				.IsRequired();

			builder.Property(e => e.Gender)
				.IsRequired();

			builder.Property(e => e.PlaceOfBirth)
				.IsRequired()
				.HasMaxLength(150);

			builder.Property(e => e.PlaceOfResidence)
				.IsRequired();

			builder.Property(e => e.DateOfIssue)
				.IsRequired();

			builder.Property(e => e.ExpirationDate)
				.IsRequired();
		}
	}
}
