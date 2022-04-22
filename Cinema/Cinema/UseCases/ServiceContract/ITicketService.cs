using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface ITicketService
    {
        Task Create(int? customerId, int chairId, decimal price);
    }
}
