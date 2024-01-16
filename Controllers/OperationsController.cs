using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesRating.Data;
using MoviesRating.Models;
using System.Threading.Tasks;

namespace MoviesRating.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ////[Authorize(Roles ="SuperAdmin")]
    public class OperationsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public OperationsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveMovie(int Id)
        {
            var movie=await dbContext.Movies.FindAsync(Id);
            if (movie == null)
                return NotFound();
             dbContext.Movies.Remove(movie);
            try
            {
               await dbContext.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Review review)
        {
            await dbContext.AddAsync(review);
            try
            {
                dbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
