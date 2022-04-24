using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationMVC.Controllers
{
    [AllowAnonymous]
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetOnScreenMoviesWithDate(int movieId, int cityId,DateTime premierDate)
        {
            return Ok();
        }

        public IActionResult GetOnScreenMovies(int movieId,int cityId)
        {
            return Ok();
        }
    }
}
