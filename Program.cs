using Kursovaya.Data;
using Kursovaya.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Kursovaya
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
					var userManager = services.GetRequiredService<UserManager<Account>>();
					var db = services.GetRequiredService<DataContext>();
					DatabaseInitializer.RoleInit(userManager, roleManager, db);
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
