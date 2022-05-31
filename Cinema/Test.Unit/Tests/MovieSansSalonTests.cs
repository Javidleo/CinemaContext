using DataAccess.Context;
using DomainModel.Domain;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
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
        private readonly MovieSansSalonValidator _validator;
        private readonly MovieFakeRepository _movieFakeRepository;
        private readonly SnasFakeRepository _sansFakeRepository;
        private readonly MovieSansSalonFakeRepository _movieSansSalonFakeRepository;
        private readonly Mock<ISalonRepository> _salonMockRepository;
        private MovieSansSalon _movieSansSalon;
        public MovieSansSalonTests()
        {
            var cinemaContext = new CinemaContext();
            _movieFakeRepository = new MovieFakeRepository(cinemaContext);
            _salonMockRepository = new Mock<ISalonRepository>();
            _sansFakeRepository = new SnasFakeRepository(cinemaContext);
            _movieSansSalonFakeRepository = new MovieSansSalonFakeRepository(cinemaContext);
            _service = new MovieSansSalonService(_movieFakeRepository, _salonMockRepository.Object, _sansFakeRepository, _movieSansSalonFakeRepository);
            _validator = new MovieSansSalonValidator();
            _movieSansSalon = new MovieSansSalonBuilder().Build();
        }

        public Salon Salon_MockSetup()
        {
            var cinema = new CinemaBuilder().Build();
            var cinemaActivity = new CinemaActivityBuilder().Build();

            var cinemaActivities = new List<CinemaActivity>() { cinemaActivity };

            var mockSalon = new Mock<Salon>();
            mockSalon.Setup(i => i.Cinema).Returns(cinema);
            mockSalon.Setup(i => i.Cinema.CinemaActivities).Returns(cinemaActivities);

            return mockSalon.Object;
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForWorkingWell()
        {
            var mockSalon = Salon_MockSetup();
            mockSalon.Cinema.CinemaActivities[0].Reactivation(); // because I know List count is 1;

            _movieFakeRepository.SetExistingId(_movieSansSalon.MovieId);
            _sansFakeRepository.SetExistingId(_movieSansSalon.SansId);
            //salon repository setup
            _salonMockRepository.Setup(i => i.FindWithParents(mockSalon.Id)).Returns(mockSalon);

            var service = new MovieSansSalonService(_movieFakeRepository, _salonMockRepository.Object, _sansFakeRepository, _movieSansSalonFakeRepository);

            var result = service.Create(_movieSansSalon.MovieId, mockSalon.Id, _movieSansSalon.SansId, _movieSansSalon.AdminGuid, _movieSansSalon.AdminFullName, _movieSansSalon.PremiereDate);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory]
        [InlineData("", "adminFullName is empty")]
        [InlineData("admin!", "invalid adminFullName")]
        [InlineData("admin123", "invalid adminFullName")]
        public void MovieSanSalonValidation_CheckForAdminFullName_ThrowValidationExcetpion(string adminFullName , string exceptionMessage)
        {
            var movieSansSalon = new MovieSansSalonBuilder().WithAdminFullName(adminFullName).Build();
            var result = _validator.TestValidate(movieSansSalon);
            result.ShouldHaveValidationErrorFor(i => i.AdminFullName).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData("12/31/9999")]
        [InlineData("1/1/0001")]
        public void MovieSansSalonValidtaion_CheckForAdminPremiereDate_ThrowValidationException(string date)
        {
            DateTime date1 = DateTime.Parse(date);
            var movieSansSalon = new MovieSansSalonBuilder().WithPremiereDate(date1).Build();
            var result = _validator.TestValidate(movieSansSalon);
            result.ShouldHaveValidationErrorFor(i => i.PremiereDate);
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForNonExistMovieid_ThrowNotFoundException()
        {
            void result() => _service.Create(_movieSansSalon.MovieId, _movieSansSalon.SalonId, _movieSansSalon.SansId, _movieSansSalon.AdminGuid, _movieSansSalon.AdminFullName, _movieSansSalon.PremiereDate);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("movie not found");
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForNonExistingSansId_ThrowsNotFoundException()
        {
            _movieFakeRepository.SetExistingId(_movieSansSalon.MovieId);

            void result() => _service.Create(_movieSansSalon.MovieId, _movieSansSalon.SalonId, _movieSansSalon.SansId, _movieSansSalon.AdminGuid, _movieSansSalon.AdminFullName, _movieSansSalon.PremiereDate);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("sans not found");
        }


        [Fact]
        public void CreateMovieSansSalon_CheckForEmptyGuid_ThrowsNotAcceptableException()
        {
            var mockSalon = Salon_MockSetup();
            mockSalon.Cinema.CinemaActivities[0].Reactivation();// becaues i know List count is 1;

            var obj = new MovieSansSalonBuilder().WithAdminGuid(Guid.Empty).Build();

            _movieFakeRepository.SetExistingId(obj.MovieId);
            _sansFakeRepository.SetExistingId(obj.SansId);

            _salonMockRepository.Setup(i => i.FindWithParents(mockSalon.Id)).Returns(mockSalon);

            var service = new MovieSansSalonService(_movieFakeRepository, _salonMockRepository.Object, _sansFakeRepository, _movieSansSalonFakeRepository);

            void result() => service.Create(obj.MovieId, mockSalon.Id, obj.SansId, obj.AdminGuid, obj.AdminFullName, obj.PremiereDate);

            var exception = Assert.Throws<NotAcceptableException>(result);
            exception.Message.Should().Be("invalid movieSansSalon");
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForInvalidSalonId_ThrowsNotFoundException()
        {
            _movieFakeRepository.SetExistingId(_movieSansSalon.MovieId);
            _sansFakeRepository.SetExistingId(_movieSansSalon.SansId);

            void result() => _service.Create(_movieSansSalon.MovieId, _movieSansSalon.SalonId, _movieSansSalon.SansId, _movieSansSalon.AdminGuid, _movieSansSalon.AdminFullName, _movieSansSalon.PremiereDate);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("salon not found");
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForDeactivatedCienam_ThrowsNotAcceptableException()
        {
            var mockSalon = Salon_MockSetup();

            _movieFakeRepository.SetExistingId(_movieSansSalon.MovieId);
            _sansFakeRepository.SetExistingId(_movieSansSalon.SansId);

            _salonMockRepository.Setup(i => i.FindWithParents(mockSalon.Id)).Returns(mockSalon);

            void result() => _service.Create(_movieSansSalon.MovieId, mockSalon.Id, _movieSansSalon.SansId, _movieSansSalon.AdminGuid, _movieSansSalon.AdminFullName, _movieSansSalon.PremiereDate);

            var exception = Assert.Throws<NotAcceptableException>(result);
            exception.Message.Should().Be("cinema is deactivated please check the cinema status");
        }


    }
}