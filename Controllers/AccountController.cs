using Kursovaya.Data;
using Kursovaya.Identity;
using Kursovaya.Models;
using Kursovaya.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Kursovaya.Controllers
{
	public class AccountController : Controller
	{
		private readonly DataContext db;
		private readonly UserManager<Account> userManager;
		private readonly SignInManager<Account> signInManager;
		private readonly RoleManager<IdentityRole<int>> roleManager;

		public AccountController(DataContext _db, UserManager<Account> _userManager,
			SignInManager<Account> _signInManager, RoleManager<IdentityRole<int>> _roleManager)
		{
			db = _db;
			userManager = _userManager;
			signInManager = _signInManager;
			roleManager = _roleManager;
		}
		#region GET
		
		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			ViewBag.Title = "Авторизация";
			return View(new LoginViewModel() { ReturnUrl = returnUrl });
		}

		[HttpGet]
		public IActionResult Register()
		{
			ViewBag.Title = "Регистрация";
			return View();
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			ViewBag.Title = "Доступ запрещён";
			return View();
		}

		[HttpGet]
		public IActionResult ForgotPassword()
		{
			ViewBag.Title = "Восстановить пароль";
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string token)
		{
			ViewBag.Title = "Восстановить пароль";
			return View(new ResetPasswordViewModel() { Token = token });
		}
		#endregion

		#region POST
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var signInResult = await signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
				if (signInResult.Succeeded)
				{
					var user = await userManager.FindByNameAsync(vm.UserName);
					if (await userManager.IsInRoleAsync(user, "AppAdmin"))
						return RedirectToAction("Index", "AppAdmin");
					if (await userManager.IsInRoleAsync(user, "HotelAdmin"))
						return RedirectToAction("Index", "HotelAdmin");
					if (await userManager.IsInRoleAsync(user, "Visitor"))
						return RedirectToAction("Index", "Home");
				}
				else
					ModelState.AddModelError("", "Неправильный логин или пароль");
			}
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var account = new Account()
				{
					UserName = vm.UserName,
					Email = vm.Email,
					RegisteredDate = DateTime.Today
				};
				var create = await userManager.CreateAsync(account, vm.Password);
				if (create.Succeeded)
				{
					var addRole = await userManager.AddToRoleAsync(account, "Visitor");
					if (addRole.Succeeded)
					{
						await db.Visitors.AddAsync(new Visitor()
						{
							Account = account,
							FirstName = vm.FirstName,
							LastName = vm.LastName,
							MiddleName = vm.MiddleName
						});
						await db.SaveChangesAsync();
						await signInManager.PasswordSignInAsync(account, vm.Password, false, false);
						return RedirectToAction("Index", "Home");
					}
					else
					{
						foreach (var error in addRole.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
					}
				}
				else
				{
					foreach (var error in create.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			var account = await userManager.FindByEmailAsync(email);
			if (account is not null)
			{
				var token = await userManager.GeneratePasswordResetTokenAsync(account);
				var callbackUrl = Url.Action("ResetPassword", "Account", new { accountId = account.Id, token = token }, HttpContext.Request.Scheme);

				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("Kursovaya", "reset.it@mail.ru"));
				message.To.Add(new MailboxAddress("Visitor", account.Email));
				message.Subject = "Reset password";

				message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
				{
					Text = $"<a href=\"{callbackUrl}\">Восстановить пароль</a>"
				};

				using (var client = new SmtpClient())
				{
					client.Connect("smtp.mail.ru", 465, true);
					client.Authenticate("reset.it@mail.ru", "");
					client.Send(message);
					client.Disconnect(true);
				}

				return RedirectToAction("Index", "Home");
			}
			return View(model: $"Не найден пользователь с таким адресом {email}");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
		{
			if (ModelState.IsValid)
			{
				var account = await userManager.FindByNameAsync(vm.Username);
				if (account is not null)
				{
					var result = await userManager.ResetPasswordAsync(account, vm.Token, vm.NewPassword);
					if (result.Succeeded)
						return RedirectToAction("Login", "Account");
				}
				return NotFound();
			}
			return View(vm);
		}
		#endregion
	}
}
