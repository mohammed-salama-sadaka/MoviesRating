using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesRating.Models;
using MoviesRating.ViewModels;
using System.Threading.Tasks;

namespace MoviesRating.Controllers
{
    [Authorize(Roles="SuperAdmin")]
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async  Task<IActionResult> Index()
        {
            return View(await roleManager.Roles.ToListAsync());
        }

        [HttpPost]

        public async Task<IActionResult> AddRole(RoleViewModel model)
        {

            if(ModelState.IsValid)
            {
                await roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
            }
            return RedirectToAction("Index");
        }
		public async Task<IActionResult> RemoveRole(string Id)
		{
			var role = await roleManager.Roles.FirstOrDefaultAsync(i => i.Id == Id);
			if (role != null)
			{
				await roleManager.DeleteAsync(role);
			}
			return RedirectToAction("Index");
		}
	}
}
