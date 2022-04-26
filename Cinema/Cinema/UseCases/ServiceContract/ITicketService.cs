using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface ITicketService
    {
        Task Create(int? customerId, int cinemaId, int salonId, int count, int movieSansSalonId, decimal ticketPrice);
    }
}
