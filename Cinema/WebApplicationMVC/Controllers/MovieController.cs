using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.ServiceContract;

namespace WebApplicationMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieSansSalonService _movieSansSalonService;
        public MovieController(IMovieService movieService, IMovieSansSalonService movieSansSalonservice)
        {
            _movieService = movieService;
            _movieSansSalonService = movieSansSalonservice;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetOnScreenMoviesWithDate")]
        public async Task<IActionResult> GetOnScreenMoviesWithDate(int movieId, int cityId,DateTime premierDate)
        {
            var result = await _movieSansSalonService.GetMovieByCity(movieId,cityId,premierDate);

            return Ok(result);
        }

        [HttpGet("GetOnScreenMovies")]
        public async Task<IActionResult> GetOnScreenMovies(int movieId,int cityId)
        {
            var result = await _movieSansSalonService.GetMovieByCity(movieId, cityId);

            return Ok(result);
        }
    }
}
