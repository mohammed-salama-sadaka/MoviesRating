using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesRating.Models;
using MoviesRating.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MoviesRating.Controllers
{
	[Authorize(Roles = "SuperAdmin")]
	public class UserRolesController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserRolesController(UserManager<User> userManager,RoleManager<IdentityRole> roleManager)
        {
			this.userManager = userManager;
			this.roleManager = roleManager;
		}
        public async Task<IActionResult> Index()
		{
			var users = await userManager.Users.ToListAsync();
			var userRolesList = new List<UserViewModel>();
			foreach(var user in users)
			{
				userRolesList.Add(new UserViewModel
				{
					UserId = user.Id,
					FirstName = user.FirstName,
					LastName = user.LastName,
					Email = user.Email,
					UserName = user.UserName,
					Roles = await userManager.GetRolesAsync(user)
				});
			}

			return View(userRolesList);
		}
		[HttpGet]
		public async Task<IActionResult> Manage(string Id)
		{
			var user = await userManager.FindByIdAsync(Id);
			if(user==null)
			{
				ViewBag.ErrorMessage = $"User with Id={Id} cant be found";
				return View("Not Found");
			}
			ViewBag.User = user.UserName;
			List<RoleViewModel> RolesList = new List<RoleViewModel>();
			foreach(var role in roleManager.Roles)
			{
				RolesList.Add(new RoleViewModel
				{
					RoleId=role.Id,
					Name=role.Name,
					IsSelected=await userManager.IsInRoleAsync(user,role.Name),
				});
			}

			return View(RolesList);
		}
		[HttpPost]
		public async Task<IActionResult> Manage(List<RoleViewModel> model,string Id)
		{
			var user = await userManager.FindByIdAsync(Id);
			if (user == null)
			{
				return View();
			}
			var roles = await userManager.GetRolesAsync(user);
			var res = await userManager.RemoveFromRolesAsync(user,roles);
			if(!res.Succeeded)
			{
				ModelState.AddModelError("", "Cant remove prevoues Roles");
				return View(model);
			}
			res = await userManager.AddToRolesAsync(user, model.Where(i => i.IsSelected).Select(i => i.Name));
			if (! res.Succeeded)
			{
				ModelState.AddModelError("", "Cant Add new Roles");
				return View(model);
			}
			ViewBag.User = user.UserName;


			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var model = new UserViewModel();
			model.RolesViewModel = await roleManager.Roles.Select(i=>new RoleViewModel {Name=i.Name,RoleId=i.Id,IsSelected=false }).ToListAsync();

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult>Create(UserViewModel  model)
		{
			var user = await userManager.Users.FirstOrDefaultAsync(i => i.Email == model.Email);
			if(user!=null)
			{
				ModelState.AddModelError("Email", "Email address Exists.");
				return View(model);
			}
			user = new User
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				UserName = new MailAddress(model.Email).User,
				EmailConfirmed = true,

			};
			var res =await userManager.CreateAsync(user,model.Password);
			if(!res.Succeeded)
			{
                ModelState.AddModelError("", "can not add the user");
                return View(model);
            }
			
			foreach(var role in model.RolesViewModel)
			{
				if (role.IsSelected)
					await userManager.AddToRoleAsync(user, role.Name);
			}
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Remove(string Id)
		{
			var user=await userManager.FindByIdAsync(Id);
			if(user!=null)
			{
				await userManager.DeleteAsync(user);
			}
			return RedirectToAction(nameof(Index));
		}

    }
}
