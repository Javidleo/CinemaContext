using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.Services;
using Xunit;

namespace Test.Unit.Tests
{
    public class SalonActivityTests
    {
        private readonly SalonActivityService _service;
        private readonly SalonActivityFakeRepository _salonActivityFakeRepository;
        private readonly SalonFakeRepository _salonFakeRepository;
        private readonly AdminFakeRepository _adminFakeRepository;
        private readonly SalonActivityValidator _validator;
        public SalonActivityTests()
        {
            _salonFakeRepository = new SalonFakeRepository();
            _salonActivityFakeRepository = new SalonActivityFakeRepository();
            _adminFakeRepository = new AdminFakeRepository();
            _service = new SalonActivityService(_salonActivityFakeRepository, _salonFakeRepository, _adminFakeRepository);
            _validator = new SalonActivityValidator();
        }

        [Theory]
        [InlineData("", "adminFullName is empty")]
        [InlineData("admin@", "invalid adminFullName")]
        [InlineData("admin123", "invalid adminFullName")]
        public void SalonActivityValidator_CheckForAdminFullName_ThrowValidationExcetpion(string adminFullName, string exceptionMessage)
        {
            var activity = new SalonActivityBuilder().WithAdminFullName(adminFullName).Build();

            var result = _validator.TestValidate(activity);
            result.ShouldHaveValidationErrorFor(i => i.AdminFullName).WithErrorMessage(exceptionMessage);
        }

        [Fact]
        public void SalonActivityValidator_CheckForAdminGuid_ThrowsValidationException()
        {
            var activity = new SalonActivityBuilder().WithAdminGuid(Guid.Empty).Build();

            var result = _validator.TestValidate(activity);
            result.ShouldHaveValidationErrorFor(i => i.AdminGuid).WithErrorMessage("adminGuid is empty");
        }

        [Fact]
        public void SalonActivityValidator_CheckForStartDateMinValue_ThrowsValidationException()
        {
            var activity = new SalonActivityBuilder().WithStartDate(DateTime.MinValue).Build();

            var result = _validator.TestValidate(activity);
            result.ShouldHaveValidationErrorFor(i => i.StartDate).WithErrorMessage("invalid startDate");
        }

        [Fact]
        public void DeactivateSalon_CheckForWorkingWell()
        {
            var activity = new SalonActivityBuilder().Build();
            _salonFakeRepository.SetExistingId(activity.SalonId);
            _adminFakeRepository.SetExistingGuid(activity.AdminGuid);

            var result = _service.Deactivate(activity.SalonId, activity.Description, activity.StartDate, activity.AdminGuid, activity.AdminFullName);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void DeactivateSalon_CheckForInvalidSalonId_ThrowsNotFoundException()
        {
            var activity = new SalonActivityBuilder().Build();

            void result() => _service.Deactivate(activity.SalonId, activity.Description, activity.StartDate, activity.AdminGuid, activity.AdminFullName);

            var exception =Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("salon not found");
        }

        [Fact]
        public void DeactivateSalon_CheckForInvalidAdminGuid_ThorwsNotFoundException()
        {
            var activity = new SalonActivityBuilder().Build();
            _salonFakeRepository.SetExistingId(activity.SalonId);

            void result() => _service.Deactivate(activity.SalonId, activity.Description, activity.StartDate, activity.AdminGuid,
                activity.AdminFullName);

            var exception =Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("admin not found");
        }

        [Fact]
        public void DeactivateSalon_CheckForValidation_ThrowsNotAcceptableException()
        {
            var activity = new SalonActivityBuilder().WithAdminFullName("nfds@%1").Build();
            _salonFakeRepository.SetExistingId(activity.SalonId);
            _adminFakeRepository.SetExistingGuid(activity.AdminGuid);

            void result() => _service.Deactivate(activity.SalonId, activity.Description, activity.StartDate, activity.AdminGuid, activity.AdminFullName);

            Assert.Throws<NotAcceptableException>(result);
        }

        //[Fact]
        //public void 
    }
}
