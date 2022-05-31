using DataAccess.Context;
using DomainModel.Domain;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
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
        // fake repository let me to choose the behavior of repository by set properties dynamically
        private readonly TicketFakeRepository _ticketFakeRepository;
        private readonly CustomerFakeRepository _customerFakeRepository;
        private readonly MovieSansSalonFakeRepository _movieSansSalonFakeRepository;
        private readonly CinemaFakeRepository _cinemaFakeRepository; 
        private Mock<IChairRepository> _chairMockRepository;

        private readonly ITicketService _ticketService;
        private readonly Ticket _ticket;
        private readonly Mock<Chair> _mockChair;

        //fixed variables
        private int _count = 1;

        public TicketTests()
        {
            var cinemaContext = new  CinemaContext();
            _ticketFakeRepository = new TicketFakeRepository(cinemaContext);
            _customerFakeRepository = new CustomerFakeRepository(cinemaContext);
            _chairMockRepository = new Mock<IChairRepository>();
            _movieSansSalonFakeRepository = new MovieSansSalonFakeRepository(cinemaContext);
            _cinemaFakeRepository = new CinemaFakeRepository(cinemaContext);

            _ticketService = new TicketService(_ticketFakeRepository, _chairMockRepository.Object, _customerFakeRepository, _movieSansSalonFakeRepository, _cinemaFakeRepository);
            // mock Setup

            _mockChair = new Mock<Chair>();
            _mockChair.Setup(i => i.Salon).Returns(new SalonBuilder().Build());
            _mockChair.Setup(i => i.Salon.Cinema).Returns(new CinemaBuilder().Build());

            _ticket = new TicketBuilder()
                        .withCustomerId(1)
                        .WithChairId(_mockChair.Object.Id)
                        .WithSalonId(_mockChair.Object.Salon.Id)
                        .WithMovieSansSalonId(1) // in this case we dont need to create instance of this object. so hard code is the best choice
                        .WithCinemaId(_mockChair.Object.Salon.CinemaId).WithPrice(20000).Build();

            _chairMockRepository.Setup(i => i.DoesExist(_mockChair.Object.Id)).Returns(true);
            _chairMockRepository.Setup(i => i.FindBySalon(_mockChair.Object.SalonId)).Returns(new List<Chair>() { _mockChair.Object });
        }
        
        [Fact]
        public void CreateTicket_CheckForWorkingWell()
        {
            _cinemaFakeRepository.SetExistingId(_ticket.CinemaId);
            _movieSansSalonFakeRepository.SetExistingId(_ticket.MovieSansSalonId);

            _chairMockRepository = new Mock<IChairRepository>();

            var result = _ticketService.Create(_ticket.CustomerId, _mockChair.Object.Salon.CinemaId, _mockChair.Object.SalonId, _count, _ticket.MovieSansSalonId, _ticket.Price);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateTicket_CheckForInvalidCustomerId_SetCustomerIdToNull()
        {
            _cinemaFakeRepository.SetExistingId(_mockChair.Object.Salon.CinemaId);
            _movieSansSalonFakeRepository.SetExistingId(_ticket.MovieSansSalonId);

            var result = _ticketService.Create(_ticket.CustomerId, _ticket.CinemaId, _ticket.SalonId, _count, _ticket.MovieSansSalonId, _ticket.Price);

            var excpected = _ticketFakeRepository.storage.Any(i => i.CustomerId == null);
            excpected.Should().BeTrue();
        }

        [Fact]
        public void CreateTicket_CheckForInvalidCinemaId_ThrowNotFoundException()
        {
            var cinemaContext = new CinemaContext();
            IChairRepository chairRepository = new ChairFakeRepository(cinemaContext); // create a fake instance of chair to avoid setup for cineamId
            var ticket = new TicketBuilder().Build();// the general _ticket in constructor have values from mockChair so we build a new one

            var service = new TicketService(_ticketFakeRepository, chairRepository, _customerFakeRepository, _movieSansSalonFakeRepository, _cinemaFakeRepository);

            void result() => service.Create(ticket.CustomerId, ticket.CinemaId, ticket.SalonId, _count, ticket.MovieSansSalonId, ticket.Price);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("cinema not found");
        }

        [Fact]
        public void CreateTicket_CheckForInvalidMovieSansSalonId_ThrowNotFoundException()
        {
            _cinemaFakeRepository.SetExistingId(_ticket.CinemaId);
            void result() => _ticketService.Create(_ticket.CustomerId, _ticket.CinemaId, _ticket.SalonId, _count, _ticket.MovieSansSalonId, _ticket.Price);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("movie not found");
        }

        [Fact]
        public void CreateTicket_CheckForEmptyChairList_ThrowNotAcceptableException()
        {
            _cinemaFakeRepository.SetExistingId(_ticket.CinemaId);
            _movieSansSalonFakeRepository.SetExistingId(_ticket.MovieSansSalonId);
            _chairMockRepository.Setup(i => i.FindBySalon(_ticket.SalonId)).Returns(new List<Chair>());

            void result() => _ticketService.Create(_ticket.CustomerId, _ticket.CinemaId, _ticket.SalonId, _count, _ticket.MovieSansSalonId, _ticket.Price); ;

            Assert.Throws<NotAcceptableException>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CreateTicket_CheckForCreatedTicketsEqualsWithCount(int count)
        {
            _cinemaFakeRepository.SetExistingId(_ticket.CinemaId);
            _movieSansSalonFakeRepository.SetExistingId(_ticket.MovieSansSalonId);
            _chairMockRepository.Setup(i => i.FindBySalon(_mockChair.Object.SalonId)).Returns(new List<Chair>()
            {
                _mockChair.Object , new ChairBuilder().Build(), new ChairBuilder().Build()
            });

            var result = _ticketService.Create(_ticket.CustomerId, _ticket.CinemaId, _ticket.SalonId, count, _ticket.MovieSansSalonId, _ticket.Price);

            _ticketFakeRepository.storage.Count.Should().Be(count);
        }
    }
}
