using CinemaAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using UseCases.ServiceContract;


namespace CinemaAPI.Controllers
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
        public async Task<IActionResult> Post(CreateTicketDTO dto)
        {
            await _ticketService.Create(dto.CustomerId, dto.CinemaId, dto.SalonId, dto.Count, dto.MovieSansSalonId, dto.TicketPrice);
            // work on ticketOutPut Later.....

            return Ok();
        }

    }
}
