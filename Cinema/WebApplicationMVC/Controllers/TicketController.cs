using Microsoft.AspNetCore.Mvc;
using UseCases.ServiceContract;

namespace WebApplicationMVC.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(int? customerId, int cinemaId, int salonId, int count, int movieSansSalonId, decimal ticketPrice)
        {
            await _ticketService.Create(customerId, cinemaId, salonId, count, movieSansSalonId, ticketPrice);

            return Ok();
        }

    }
}
