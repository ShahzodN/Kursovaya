using Kursovaya.Data.EntityConfigurations;
using Kursovaya.Identity;
using Kursovaya.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Kursovaya.Data
{
	public class DataContext : IdentityDbContext<Account, IdentityRole<int>, int>
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		public virtual DbSet<AppAdmin> AppAdmins { get; set; }
		public virtual DbSet<BookingHistory> BookingHistories { get; set; }
		public virtual DbSet<City> Cities { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<EmployeeVisitInfo> EmployeeVisitInfos { get; set; }
		public virtual DbSet<Hotel> Hotels { get; set; }
		public virtual DbSet<Passport> Passports { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }
		public virtual DbSet<Room> Rooms { get; set; }
		public virtual DbSet<Visitor> Visitors { get; set; }
		public virtual DbSet<Blacklist> Blacklist { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new AppAdminConfig());
			builder.ApplyConfiguration(new BookingHistoryConfig());
			builder.ApplyConfiguration(new CityConfig());
			builder.ApplyConfiguration(new EmployeeConfig());
			builder.ApplyConfiguration(new EmployeeVisitInfoConfig());
			builder.ApplyConfiguration(new HotelConfig());
			builder.ApplyConfiguration(new PassportConfig());
			builder.ApplyConfiguration(new ReviewConfig());
			builder.ApplyConfiguration(new RoomConfig());
			builder.ApplyConfiguration(new VisitorConfig());
			builder.ApplyConfiguration(new BlacklistConfig());
		}
	}
}
