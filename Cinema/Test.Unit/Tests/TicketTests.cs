using DomainModel.Domain;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
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

        private readonly ITicketService _ticketService;
        private Ticket _ticket;
        private Mock<Chair> _mockChair;

        //fixed variables
        private int _count = 1;
        private int _movieSansSalonId = 1;
        public TicketTests()
        {
            _ticketFakeRepository = new TicketFakeRepository();
            _customerFakeRepository = new CustomerFakeRepository();
            _chairMockRepository = new Mock<IChairRepository>();
            _movieSansSalonFakeRepository = new MovieSansSalonFakeRepository();
            _cinemaFakeRepository = new CinemaFakeRepository();

            _ticketService = new TicketService(_ticketFakeRepository, _chairMockRepository.Object, _customerFakeRepository, _movieSansSalonFakeRepository, _cinemaFakeRepository);

            var salon = new SalonBuilder().Build();
            var cinema = new CinemaBuilder().Build();
            _mockChair = new Mock<Chair>();
            _mockChair.Setup(i => i.Salon).Returns(salon);
            _mockChair.Setup(i => i.Salon.Cinema).Returns(cinema);

            _ticket = new TicketBuilder()
                            .withCustomerId(1)
                            .WithChairId(_mockChair.Object.Id)
                            .WithSalonId(_mockChair.Object.Salon.Id)
                            .WithCinemaId(cinema.Id).WithPrice(20000).Build();

            _chairMockRepository.Setup(i => i.DoesExist(_mockChair.Object.Id)).Returns(true);
            _chairMockRepository.Setup(i => i.FindBySalon(_mockChair.Object.SalonId)).Returns(new List<Chair>() { _mockChair.Object });
        }



        [Fact]
        public void CreateTicket_CheckForWorkingWell()
        {
            _customerFakeRepository.SetExistingId(_ticket.CustomerId.Value);
            _cinemaFakeRepository.SetExistingId(_mockChair.Object.Salon.CinemaId);
            _movieSansSalonFakeRepository.SetExistingId(_movieSansSalonId);

            _chairMockRepository = new Mock<IChairRepository>();

            var result = _ticketService.Create(_ticket.CustomerId, _mockChair.Object.Salon.CinemaId, _mockChair.Object.SalonId, _count, _movieSansSalonId, _ticket.Price);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateTicket_CheckForInvalidCustomerId_SetCustomerIdToNull()
        {


            var excpected = _ticketFakeRepository.storage.Any(i => i.CustomerId == null);
            excpected.Should().BeTrue();
        }


    }
}
