using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class AppAdminConfig : IEntityTypeConfiguration<AppAdmin>
	{
		public void Configure(EntityTypeBuilder<AppAdmin> builder)
		{
			builder.ToTable("AppAdmins");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.LastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.HasOne(e => e.Account)
				.WithOne(e => e.AppAdmin)
				.HasForeignKey<AppAdmin>(e => e.AccountId);
		}
	}
}
