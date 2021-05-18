using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class VisitorConfig : IEntityTypeConfiguration<Visitor>
	{
		public void Configure(EntityTypeBuilder<Visitor> builder)
		{
			builder.ToTable("Visitors");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.LastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.MiddleName)
				.HasMaxLength(50);

			builder.HasOne(e => e.Account)
				.WithOne(e => e.Visitor)
				.HasForeignKey<Visitor>(e => e.AccountId);

			builder.HasOne(e => e.Passport)
				.WithOne(e => e.Visitor)
				.HasForeignKey<Visitor>(e => e.PassportId);
		}
	}
}
