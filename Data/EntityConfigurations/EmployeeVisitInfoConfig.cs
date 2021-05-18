using Kursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kursovaya.Data.EntityConfigurations
{
	public class EmployeeVisitInfoConfig : IEntityTypeConfiguration<EmployeeVisitInfo>
	{
		public void Configure(EntityTypeBuilder<EmployeeVisitInfo> builder)
		{
			builder.ToTable("EmployeeVisitInfos");

			builder.HasKey(e => e.Id);

			builder.Property(e => e.CheckIn);

			builder.Property(e => e.CheckOut);

			builder.HasOne(e => e.Employee)
				.WithMany(e => e.VisitInfos)
				.HasForeignKey(e => e.EmployeeId);
		}
	}
}
