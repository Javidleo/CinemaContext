using FluentAssertions;
using System;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
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
        private readonly AdminFakeRepository _adminFakeRepository;
        private readonly MovieSansSalonFakeRepository _movieSansSalonFakeRepository;
        public MovieSansSalonTests()
        {
            _movieFakeRepository = new MovieFakeRepository();
            _salonFakeRepository = new SalonFakeRepository();
            _sansFakeRepository = new SnasFakeRepository();
            _adminFakeRepository = new AdminFakeRepository();
            _movieSansSalonFakeRepository = new MovieSansSalonFakeRepository();
            _service = new MovieSansSalonService(_movieFakeRepository, _salonFakeRepository, _sansFakeRepository
                                                ,_adminFakeRepository,_movieSansSalonFakeRepository);
        }
        [Fact]
        public void CreateMovieSansSalon_CheckForWorkingWell()
        {
            // obj is a MovieSansSalon we use obj because its a short name
            var obj = new MovieSansSalonBuilder().Build();
            _movieFakeRepository.SetExistingId(obj.MovieId);
            _salonFakeRepository.SetExistingId(obj.SalonId);
            _sansFakeRepository.SetExistingId(obj.SansId);

            var result = _service.Create(obj.MovieId, obj.SalonId, obj.SansId, obj.AdminGuid);
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
        public void CreateMovieSansSalon_CheckForNonExistingSalonId_ThrowNotFoundException()
        {
            var obj = new MovieSansSalonBuilder().Build();
            _movieFakeRepository.SetExistingId(obj.MovieId);

            void result() => _service.Create(obj.MovieId, obj.SalonId, obj.SansId, obj.AdminGuid);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("salon not found");
        }

        [Fact]
        public void CreateMovieSansSalon_CheckForNonExistingSansId_ThrowsNotFoundException()
        {
            var obj = new MovieSansSalonBuilder().Build();
            _movieFakeRepository.SetExistingId(obj.MovieId);
            _salonFakeRepository.SetExistingId(obj.SalonId);

            void result() => _service.Create(obj.MovieId, obj.SalonId, obj.SansId, obj.AdminGuid);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("sans not found");
        }


        [Fact]
        public void CreateMovieSansSalon_CheckForEmptyGuid_ThrowsNotAcceptableException()
        {
            var obj = new MovieSansSalonBuilder().WithAdminGuid(Guid.Empty).Build();

            _movieFakeRepository.SetExistingId(obj.MovieId);
            _salonFakeRepository.SetExistingId(obj.SalonId);
            _sansFakeRepository.SetExistingId(obj.SansId);

            void result() => _service.Create(obj.MovieId, obj.SalonId, obj.SansId, obj.AdminGuid);
            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void GetMovieByCity_CheckForExcpectedValue()
        {
            var result = _service.GetMovieByCity(movieId: 1, cityId: 1);
        }
    }
}