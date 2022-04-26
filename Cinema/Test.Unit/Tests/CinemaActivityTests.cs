using FluentAssertions;
using System;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.ServiceContract;
using Xunit;

namespace Test.Unit.Tests
{
    public class CinemaActivityTests
    {
        private readonly CinemaActivityService _cinemaActivityService;
        private readonly CinemaFakeRepository _cinemaFakeRepository;
        private readonly CinemaActivityFakeRepository _cinemaActivityFakeRepository;
        public CinemaActivityTests()
        {
            _cinemaFakeRepository = new CinemaFakeRepository();
            _cinemaActivityFakeRepository = new CinemaActivityFakeRepository();
            _cinemaActivityService = new CinemaActivityService(_cinemaFakeRepository, _cinemaActivityFakeRepository);
        }
        [Fact]
        public void DeactiveCinema_CheckForWorkingWell()
        {
            var cinemaActivity = new CinemaActivityBuilder().Build();
            _cinemaFakeRepository.SetExistingId(cinemaActivity.CinemaId);

            var result = _cinemaActivityService.Deactivate(
                        cinemaActivity.CinemaId, cinemaActivity.Description, cinemaActivity.AdminGuid, cinemaActivity.AdminFullName);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void DeactiveCinema_CheckForInvalidCinemaId_ThrowsNotFoundException()
        {
            var cinemaActivity = new CinemaActivityBuilder().Build();

            void result() => _cinemaActivityService.Deactivate(
                            cinemaActivity.CinemaId, cinemaActivity.Description, cinemaActivity.AdminGuid, cinemaActivity.AdminFullName);
            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void DeactiveCinema_CheckForEmptyAdminGuid_ThrowsNotAcceptableException()
        {
            var cinemaActivity = new CinemaActivityBuilder().WithAdminGuid(Guid.Empty).Build();
            _cinemaFakeRepository.SetExistingId(cinemaActivity.CinemaId);

            void result() => _cinemaActivityService.Deactivate(
                            cinemaActivity.CinemaId, cinemaActivity.Description, cinemaActivity.AdminGuid, cinemaActivity.AdminFullName);

            var exception = Assert.Throws<NotAcceptableException>(result);
            exception.Message.Should().Be("invalid adminId");
        }

        [Fact]
        public void DeactiveCinema_CheckForNullAdminFullName_ThrowsNOtAcceptableException()
        {
            var cinemaActivity = new CinemaActivityBuilder().WithAdminFullName("").Build();
            _cinemaFakeRepository.SetExistingId(cinemaActivity.CinemaId);

            void result() => _cinemaActivityService.Deactivate(
                            cinemaActivity.CinemaId, cinemaActivity.Description, cinemaActivity.AdminGuid, cinemaActivity.AdminFullName);

            var exception = Assert.Throws<NotAcceptableException>(result);
            exception.Message.Should().Be("admin name is empty");
        }

    }
}
