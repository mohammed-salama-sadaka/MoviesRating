using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesRating.Data;
using MoviesRating.Models;
using System;
using System.Threading.Tasks;

namespace MoviesRating.Controllers
{
    [Authorize()]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;

        public HomeController(ApplicationDbContext dbContext,UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await dbContext.Movies.Include(i => i.Reviews).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var movie =await dbContext.Movies.Include(i=>i.Reviews).FirstOrDefaultAsync(i=>i.Id==Id);
            if(movie==null)
                return NotFound();
            return View(movie);
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(int Id)
        {
            var movie = await dbContext.Movies.FindAsync(Id);
            if (movie != null)
            {
                var review = new Review
                {
                    Rate = double.Parse(Request.Form["Rating"]),
                    Comment = Request.Form["comment"],
                    Movie=movie,
                    User= await userManager.GetUserAsync(HttpContext.User),
                    AddDate=DateTime.Now,
                };
                await dbContext.Reviews.AddAsync(review);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details),new {Id=movie.Id});
        }
    }
}
