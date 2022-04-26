using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.ServiceContract;

namespace WebApplicationMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetOnScreenMoviesWithDate(int movieId, int cityId,DateTime premierDate)
        {
            
        }

        public IActionResult GetOnScreenMovies(int movieId,int cityId)
        {
            return Ok();
        }
    }
}
