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
            _ticketService = new TicketService(_ticketFakeRepository, _chairMockRepository.Object, _customerFakeRepository); ;
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

            var result = _ticketService.Create(_ticket.CustomerId, _ticket.ChairId, _ticket.Price);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateTicket_CheckForExistingCustomerId_SendForCreatingIfExist()
        {
            _customerFakeRepository.SetExistingId(_ticket.CustomerId.Value);

            var result = _ticketService.Create(_ticket.CustomerId, _ticket.ChairId, _ticket.Price);

            var excpected = _ticketFakeRepository.storage.First();
            excpected.Should().BeEquivalentTo(_ticket);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        // if you pass null for custoemrId then system assumes you are a guest
        public void CreateTicket_CheckForInvalidCustomerId_PassNullAndCreateAsGuess()
        {
            var result = _ticketService.Create(_ticket.CustomerId, _ticket.ChairId, _ticket.Price);

            var excpected = _ticketFakeRepository.storage.First();
            excpected.CustomerId.Should().BeNull();
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateTicket_CheckForExistingChairWithIncludingParents_ThrowNotFoundException_NothingFound()
        {
            _mockChair.Setup(i => i.Salon);
            _chairMockRepository.Setup(i => i.FindWithParents(_ticket.ChairId)).Returns(_mockChair.Object);

            void result() => _ticketService.Create(_ticket.CustomerId, _ticket.ChairId, _ticket.Price);

            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void CreateTicket_CheckForExistingChairWithIncludingParents_ReturnExcpectedValue()
        {
            _mockChair.Setup(i => i.Salon).Returns(_salon);

            _chairMockRepository.Setup(i => i.FindWithParents(_ticket.ChairId)).Returns(_mockChair.Object);

            var result = _ticketService.Create(_ticket.CustomerId, _ticket.ChairId, _ticket.Price);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateTicket_CheckForDisabledChair_ThrowsNotAcceptableException()
        {
            _mockChair.Setup(i => i.Salon).Returns(_salon);
            _mockChair.Object.Disable();
            _chairMockRepository.Setup(i => i.FindWithParents(_ticket.ChairId)).Returns(_mockChair.Object);

            void result() => _ticketService.Create(_ticket.CustomerId, _ticket.ChairId, _ticket.Price);

            Assert.Throws<NotAcceptableException>(result);
        }
    }
}
