using DomainModel.Domain;
using FluentAssertions;
using Moq;
using System.Linq;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;
using UseCases.Services;
using Xunit;

namespace Test.Unit.Tests
{
    public class TicketTests
    {
        private readonly TicketFakeRepository _ticketFakeRepository;
        private readonly CustomerFakeRepository _customerFakeRepository;
        private readonly MovieSansSalonFakeRepository _movieSansSalonFakeRepository;
        private readonly CinemaFakeRepository _cinemaFakeRepository;
        private Mock<IChairRepository> _chairMockRepository;
        private Salon _salon;
        private Cinema _cinema;
        private Mock<Chair> _mockChair;
        private Ticket _ticket;
        private readonly ITicketService _ticketService;
        public TicketTests()
        {
            _ticketFakeRepository = new TicketFakeRepository();
            _customerFakeRepository = new CustomerFakeRepository();
            _chairMockRepository = new Mock<IChairRepository>();
            _movieSansSalonFakeRepository = new MovieSansSalonFakeRepository();
            _cinemaFakeRepository = new CinemaFakeRepository();
            _ticketService = new TicketService(_ticketFakeRepository, _chairMockRepository.Object, _customerFakeRepository,                                                         _movieSansSalonFakeRepository,_cinemaFakeRepository);
            // mock setup
            _salon = new SalonBuilder().Build();
            _cinema = new CinemaBuilder().Build();
            _mockChair = new Mock<Chair>();
            _mockChair.Setup(i => i.Salon).Returns(_salon);
            _mockChair.Setup(i => i.Salon.Cinema).Returns(_cinema);

            _ticket = new TicketBuilder()
                            .withCustomerId(1)
                            .WithChairId(_mockChair.Object.Id)
                            .WithSalonId(_mockChair.Object.Salon.Id)
                            .WithCinemaId(_cinema.Id).WithPrice(20000).Build();

            _chairMockRepository.Setup(i => i.DoesExist(_mockChair.Object.Id)).Returns(true);
            _chairMockRepository.Setup(i => i.FindWithParents(_mockChair.Object.Id)).Returns(_mockChair.Object);

        }

        [Fact]
        public void CreateTicket_CheckForWorkingWell()
        {
            _customerFakeRepository.SetExistingId(_ticket.CustomerId.Value);
            _cinemaFakeRepository.SetExistingId(_cinema.Id);
            _movieSansSalonFakeRepository.SetExistingId(1);
            int count = 1;

            var result = _ticketService.Create(_ticket.CustomerId, _cinema.Id, _salon.Id, count, 1, _ticket.Price);

            result.Status.ToString().Should().Be("RanToCompletion");
        }
    }
}
