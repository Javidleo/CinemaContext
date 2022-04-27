using Microsoft.AspNetCore.Mvc;
using UseCases.ServiceContract;

namespace WebApplicationMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieSansSalonController : ControllerBase
    {
        private readonly IMovieSansSalonService _movieSansSalonService;
        public MovieSansSalonController(IMovieSansSalonService movieSansSalonservice)
        {
            _movieSansSalonService = movieSansSalonservice;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _movieSansSalonService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetOnScreenMoviesWithDate")]
        public async Task<IActionResult> GetOnScreenMoviesWithDate(int movieId, int cityId, DateTime premierDate)
        {
            var result = await _movieSansSalonService.GetMovieByCity(movieId, cityId, premierDate);

            return Ok(result);
        }

        [HttpGet("GetOnScreenMovies")]
        public async Task<IActionResult> GetOnScreenMovies(int movieId, int cityId)
        {
            var result = await _movieSansSalonService.GetMovieByCity(movieId, cityId);

            return Ok(result);
        }
    }
}
