using DomainModel.Domain;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.Services;
using Xunit;

namespace Test.Unit.Tests
{
    public class MovieSansSalonTests
    {
        private readonly MovieSansSalonService _service;
        private readonly MovieFakeRepository _movieFakeRepository;
        private readonly SalonFakeRepository _salonFakeRepository;
        private readonly SnasFakeRepository _sansFakeRepository;
        private readonly MovieSansSalonFakeRepository _movieSansSalonFakeRepository;
        public MovieSansSalonTests()
        {
            _movieFakeRepository = new MovieFakeRepository();
            _salonFakeRepository = new SalonFakeRepository();
            _sansFakeRepository = new SnasFakeRepository();
            _movieSansSalonFakeRepository = new MovieSansSalonFakeRepository();
            _service = new MovieSansSalonService(_movieFakeRepository, _salonFakeRepository, _sansFakeRepository, _movieSansSalonFakeRepository);
        }

        public Salon CreateMovieSansSalon_MockSetup()
        {
            var cinema = new CinemaBuilder().Build();
            var cinemaActivity = new List<CinemaActivity>() { new CinemaActivityBuilder().WithEndDate(DateTime.Now.Date).Build() };

            var mockSalon = new Mock<Salon>();
            mockSalon.Setup(i => i.Cinema).Returns(cinema);
            mockSalon.Setup(i => i.Cinema.CinemaActivities).Returns(cinemaActivity);

            return mockSalon.Object;
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForWorkingWell()
        {
            var mockSalon = CreateMovieSansSalon_MockSetup();

            // obj is a MovieSansSalon we use obj because its a short name
            var obj = new MovieSansSalonBuilder().Build();
            _movieFakeRepository.SetExistingId(obj.MovieId);
            _sansFakeRepository.SetExistingId(obj.SansId);

            var salonMockRepository = new Mock<ISalonRepository>();
            salonMockRepository.Setup(i => i.FindWithParents(mockSalon.Id)).Returns(mockSalon);

            var service = new MovieSansSalonService(_movieFakeRepository, salonMockRepository.Object, _sansFakeRepository, _movieSansSalonFakeRepository);


            var result = service.Create(obj.MovieId, mockSalon.Id, obj.SansId, obj.AdminGuid);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForNonExistMovieid_ThrowNotFoundException()
        {
            var obj = new MovieSansSalonBuilder().Build();

            void result() => _service.Create(obj.MovieId, obj.SalonId, obj.SansId, obj.AdminGuid);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("movie not found");
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForNonExistingSansId_ThrowsNotFoundException()
        {
            var obj = new MovieSansSalonBuilder().Build();
            _movieFakeRepository.SetExistingId(obj.MovieId);

            void result() => _service.Create(obj.MovieId, obj.SalonId, obj.SansId, obj.AdminGuid);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("sans not found");
        }


        [Fact]
        public void CreateMovieSansSalon_CheckForEmptyGuid_ThrowsNotAcceptableException()
        {
            var obj = new MovieSansSalonBuilder().WithAdminGuid(Guid.Empty).Build();

            _movieFakeRepository.SetExistingId(obj.MovieId);
            _sansFakeRepository.SetExistingId(obj.SansId);

            void result() => _service.Create(obj.MovieId, obj.SalonId, obj.SansId, obj.AdminGuid);
            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForInvalidSalonId_ThrowsNotFoundException()
        {
            var obj = new MovieSansSalonBuilder().Build();

            _movieFakeRepository.SetExistingId(obj.MovieId);
            _sansFakeRepository.SetExistingId(obj.SansId);

            void result() => _service.Create(obj.MovieId, obj.SalonId, obj.SansId, obj.AdminGuid);
            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForDeactivatedCienam_ThrowsNotAcceptableException()
        {
            var obj = new MovieSansSalonBuilder().Build();

            _movieFakeRepository.SetExistingId(obj.MovieId);
            _sansFakeRepository.SetExistingId(obj.SansId);

            var mockSalon = new Mock<Salon>();
            var cinema = new CinemaBuilder().Build();
            var cinemaActivity = new List<CinemaActivity>() { new CinemaActivityBuilder().WithEndDate(null).Build() };

            mockSalon.Setup(i => i.Cinema).Returns(cinema);
            mockSalon.Setup(i => i.Cinema.CinemaActivities).Returns(cinemaActivity);

            var mockSalonRepository = new Mock<ISalonRepository>();
            mockSalonRepository.Setup(i => i.FindWithParents(mockSalon.Object.Id)).Returns(mockSalon.Object);

            var service = new MovieSansSalonService(_movieFakeRepository, mockSalonRepository.Object, _sansFakeRepository, _movieSansSalonFakeRepository);

            void result() => service.Create(obj.MovieId, mockSalon.Object.Id, obj.SansId, obj.AdminGuid);

            Assert.Throws<NotAcceptableException>(result);
        }

        //[Fact]

        //public void GetMovieByCity_CheckForExcpectedValue()
        //{
        //    var result = _service.GetMovieByCity(movieId: 1, cityId: 1);
        //}
    }
}