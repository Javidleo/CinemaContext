using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.ServiceContract;

namespace Application.Controllers
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
            if (dto is null)
                throw new NotAcceptableException("invalid input");

            await _ticketService.Create(dto.CustomerId, dto.ChairId, dto.Price);
            return Ok();
        }
    }
}
