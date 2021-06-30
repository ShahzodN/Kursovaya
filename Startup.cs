using Kursovaya.Data;
using Kursovaya.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kursovaya
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddDbContext<DataContext>(config =>
			{
				config.UseSqlServer(Configuration.GetConnectionString("sqlserver")).UseLazyLoadingProxies();
			});
			services.AddIdentity<Account, IdentityRole<int>>(config =>
			{
				config.Password.RequireDigit = false;
				config.Password.RequiredLength = 5;
				config.Password.RequireLowercase = false;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequireUppercase = false;
				config.User.RequireUniqueEmail = true;
			})
			.AddEntityFrameworkStores<DataContext>()
			.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(config =>
			{
				config.LoginPath = "/Account/Login";
				config.AccessDeniedPath = "/Account/AccessDenied";
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
