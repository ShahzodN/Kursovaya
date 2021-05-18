using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class EmployeeConfig : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.ToTable("Employees");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.FirstName)
				.IsRequired()
				.HasMaxLength(50);
			
			builder.Property(e => e.LastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(e => e.MiddleName)
				.HasMaxLength(50);

			builder.Property(e => e.Position)
				.IsRequired();

			builder.Property(e => e.Gender)
				.IsRequired();

			builder.HasOne(e => e.Account)
				.WithOne(e => e.Employee)
				.HasForeignKey<Employee>(e => e.AccountId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(e => e.Hotel)
				.WithMany(e => e.Employees)
				.HasForeignKey(e => e.HotelId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(e => e.Passport)
				.WithOne(e => e.Employee)
				.HasForeignKey<Employee>(e => e.PassportId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
