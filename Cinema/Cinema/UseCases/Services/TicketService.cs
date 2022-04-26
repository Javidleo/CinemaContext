using DomainModel.Domain;
using System.Collections.Generic;
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
        private readonly IMovieSansSalonRepository _movieSansSalonRepository;
        private readonly ICinemaRepository _cinemaRepository;

        public TicketService(ITicketRepository ticketRepository, IChairRepository chairRepository, ICustomerRepository customerRepository, IMovieSansSalonRepository movieSansSalonRepository, ICinemaRepository cinemaRepository)
        {
            _ticketRepository = ticketRepository;
            _chairRepository = chairRepository;
            _customerRepository = customerRepository;
            _movieSansSalonRepository = movieSansSalonRepository;
            _cinemaRepository = cinemaRepository;
        }

        public Task Create(int? customerId, int cinemaId, int salonId, int count, int movieSansSalonId, decimal ticketPrice)
        {
            if (!_customerRepository.DoesExist(customerId))
                customerId = null;

            if (!_cinemaRepository.DoesExist(cinemaId))
                throw new NotFoundException("cinema not found");

            if (_movieSansSalonRepository.DoesExist(movieSansSalonId))
                throw new NotFoundException("movie not found");

            var chairs = _chairRepository.FindBySalon(salonId);

            if (chairs is null)
                throw new NotFoundException("invalid salon");

            List<Ticket> ticketList = new List<Ticket>();

            for (int i = 0; i < count; i++)
            {
                var ticket = Ticket.Create(customerId, chairs[i].Id, salonId, cinemaId, movieSansSalonId, ticketPrice);
                ticketList.Add(ticket);
            }
            _ticketRepository.Add(ticketList);
            return Task.CompletedTask;
        }
    }
}
