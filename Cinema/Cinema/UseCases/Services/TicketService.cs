using DomainModel.Domain;
using DomainModel.Validation;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IChairRepository _chairRepository;
        private readonly ICustomerRepository _customerRepository;

        public TicketService(ITicketRepository ticketRepository, IChairRepository chairRepository, ICustomerRepository customerRepository)
        {
            _ticketRepository = ticketRepository;
            _chairRepository = chairRepository;
            _customerRepository = customerRepository;
        }

        public Task Create(int? customerId, int chairId, decimal price)
        {
            if (!_customerRepository.DoesExist(customerId))
                customerId = null;

            var chair = _chairRepository.FindWithParents(chairId);

            if (chair is null || chair.Salon is null)
                throw new NotFoundException("invalid chair");

            if (chair.IsDisabled ==true)
                throw new NotAcceptableException("this chair is not available");

            var ticket = Ticket.Create(customerId, chairId, chair.SalonId, chair.Salon.CinemaId, price);

            _ticketRepository.Add(ticket);
            return Task.CompletedTask;
        }
    }
}
