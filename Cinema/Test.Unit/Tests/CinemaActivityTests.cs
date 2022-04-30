using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
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
        private readonly CinemaActivityService _service;
        private readonly CinemaActivityValidator _validator;
        private readonly CinemaFakeRepository _cinemaFakeRepository;
        private readonly AdminFakeRepository _adminFakeRepository;
        private readonly CinemaActivityFakeRepository _cinemaActivityFakeRepository;
        public CinemaActivityTests()
        {
            _cinemaFakeRepository = new CinemaFakeRepository();
            _cinemaActivityFakeRepository = new CinemaActivityFakeRepository();
            _adminFakeRepository = new AdminFakeRepository();
            _service = new CinemaActivityService(_cinemaFakeRepository, _cinemaActivityFakeRepository, _adminFakeRepository);

            _validator = new CinemaActivityValidator();
        }

        [Theory]
        [InlineData("", "adminFullName is empty")]
        [InlineData("fs@fr", "invalid adminFullName")]
        [InlineData("fer11 , ", "invalid adminFullName")]
        public void CinemaActivityValidation_CheckForAdminFullName_ThrowValidationException(string adminFullName, string exceptionMessage)
        {
            var cinemaActivity = new CinemaActivityBuilder().WithAdminFullName(adminFullName).Build();

            var result = _validator.TestValidate(cinemaActivity);
            result.ShouldHaveValidationErrorFor(i => i.AdminFullName).WithErrorMessage(exceptionMessage);
        }

        [Fact]
        public void CinemaActivityValidation_CheckForEmptyAdminGuid_ThrowValidationException()
        {
            var cinemaActivity = new CinemaActivityBuilder().WithAdminGuid(Guid.Empty).Build();

            var result = _validator.TestValidate(cinemaActivity);
            result.ShouldHaveValidationErrorFor(i => i.AdminGuid).WithErrorMessage("adminGuid is empty");
        }

        [Theory]
        [InlineData("", "startDatePersian is empty")]
        [InlineData("1222.33.22", "invalid startDatePersian")]
        [InlineData("33/1231/12", "invalid startDatePersian")]
        [InlineData("11/11/11", "invalid startDatePersian")]
        public void CinemaActivityValidation_CheckForStartDatePersian_ThrowValidtaionException(string startDate, string excetpionMessage)
        {
            var cinemaActivity = new CinemaActivityBuilder().WithPersianStartDate(startDate).Build();

            var result = _validator.TestValidate(cinemaActivity);
            result.ShouldHaveValidationErrorFor(i => i.StartDatePersian).WithErrorMessage(excetpionMessage);
        }

        [Fact]
        public void CinemaActivityValidation_CheckForStartDateInMaxValue_ThrowValidationException()
        {
            var cinemaActivity = new CinemaActivityBuilder().WithStartDate(DateTime.MaxValue).Build();

            var result = _validator.TestValidate(cinemaActivity);
            result.ShouldHaveValidationErrorFor(i => i.StartDate).WithErrorMessage("invalid startDate");
        }

        [Fact]
        public void CinemaActivityValidation_CheckForStartDateInMinValue_ThrowValidationException()
        {
            var cinemaActivity = new CinemaActivityBuilder().WithStartDate(DateTime.MinValue).Build();

            var result = _validator.TestValidate(cinemaActivity);
            result.ShouldHaveValidationErrorFor(i => i.StartDate).WithErrorMessage("invalid startDate");
        }

        [Fact]
        public void DeactivateCinema_CheckForWorkingWell()
        {
            var cinemaActivity = new CinemaActivityBuilder().Build();
            _cinemaFakeRepository.SetExistingId(cinemaActivity.CinemaId);
            _adminFakeRepository.SetExistingGuid(cinemaActivity.AdminGuid);

            var result = _service.Deactivate(cinemaActivity.CinemaId, cinemaActivity.Description,cinemaActivity.StartDate, cinemaActivity.AdminGuid, cinemaActivity.AdminFullName);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void DeactivateCinema_CheckForInvalidCinemaId_ThrowsNotFoundException()
        {
            var cinemaActivity = new CinemaActivityBuilder().Build();

            void result() => _service.Deactivate(cinemaActivity.CinemaId, cinemaActivity.Description, cinemaActivity.StartDate, cinemaActivity.AdminGuid, cinemaActivity.AdminFullName);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("cinema not found");
        }

        [Fact]
        public void DeactivateCinema_CheckForInvalidAdminGuid_ThrowsNotFoundException()
        {
            var cinemaActivity = new CinemaActivityBuilder().Build();
            _cinemaFakeRepository.SetExistingId(cinemaActivity.CinemaId);

            void result() => _service.Deactivate(cinemaActivity.CinemaId, cinemaActivity.Description, cinemaActivity.StartDate, cinemaActivity.AdminGuid, cinemaActivity.AdminFullName);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("admin not found");
        }
    }
}
