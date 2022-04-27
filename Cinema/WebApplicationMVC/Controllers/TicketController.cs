using Microsoft.AspNetCore.Mvc;
using UseCases.ServiceContract;

namespace WebApplicationMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int? customerId, int cinemaId, int salonId, int count, int movieSansSalonId, decimal ticketPrice)
        {
            await _ticketService.Create(customerId, cinemaId, salonId, count, movieSansSalonId, ticketPrice);

            return Ok();
        }

    }
}
