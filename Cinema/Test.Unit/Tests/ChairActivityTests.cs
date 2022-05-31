using DataAccess.Context;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.Services;
using Xunit;

namespace Test.Unit.Tests
{
    public class ChairActivityTests
    {
        private readonly ChairActivityService _service;
        private readonly ChairActivityValidator _validator;
        private readonly ChairFakeRepository _chairFakeRepository;
        private readonly AdminFakeRepository _adminFakeRepository;
        private readonly ChairActivityFakeRepository _chairActivityFakeRepository;
        private readonly SalonFakeRepository _salonFakeRepository;
        private List<int> _chairIdList;
        public ChairActivityTests()
        {
            var cinemaContext = new CinemaContext();
            _chairFakeRepository = new ChairFakeRepository(cinemaContext);
            _adminFakeRepository = new AdminFakeRepository(cinemaContext);
            _chairActivityFakeRepository = new ChairActivityFakeRepository(cinemaContext);
            _salonFakeRepository = new SalonFakeRepository(cinemaContext);
            _service = new ChairActivityService(_chairFakeRepository, _adminFakeRepository, _chairActivityFakeRepository,
                    _salonFakeRepository);
            _validator = new ChairActivityValidator();
            _chairIdList = new List<int>() { 1, 2, 3, 4, 5 };
        }
        [Fact]
        public void DeactivateChair_CheckForWorkingWell()
        {
            var activity = new ChairActivityBuilder().Build();
            _chairFakeRepository.SetExistingId(activity.ChairId);
            _adminFakeRepository.SetExistingGuid(activity.AdminGuid);

            var result = _service.Deactivate(activity.ChairId, activity.Description, activity.StartDate, activity.AdminGuid,
                activity.AdminFullName);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory]
        [InlineData("", "adminFullName is empty")]
        [InlineData("admin134", "invalid adminFullName")]
        [InlineData("admin@52", "invalid adminFullName")]
        public void ChairActivityValidator_CheckForAdminFullName_ThrowValidtaionException(string adminFullName, string exceptionMessage)
        {
            var activity = new ChairActivityBuilder().WithAdminFullName(adminFullName).Build();

            var result = _validator.TestValidate(activity);
            result.ShouldHaveValidationErrorFor(i => i.AdminFullName).WithErrorMessage(exceptionMessage);
        }

        [Fact]
        public void ChairActivityValidator_CheckForAdminFullName_ThrowValidationExcetpion()
        {
            var activity = new ChairActivityBuilder().WithAdminGuid(Guid.Empty).Build();

            var result = _validator.TestValidate(activity);
            result.ShouldHaveValidationErrorFor(i => i.AdminGuid).WithErrorMessage("adminGuid is empty");
        }

        [Fact]
        public void ChairActivityValidator_CheckForStartDate_ThrowValidtaionException()
        {
            var activity = new ChairActivityBuilder().WithStartDate(DateTime.MaxValue).Build();

            var result = _validator.TestValidate(activity);
            result.ShouldHaveValidationErrorFor(i => i.StartDate).WithErrorMessage("invalid startDate");
        }

        [Theory]
        [InlineData("", "startDatePersian is empty")]
        [InlineData("11/11/11", "invalid startDatePersian")]
        [InlineData("1111/1111/11", "invalid startDatePersian")]
        [InlineData("123123/11/11", "invalid startDatePersian")]
        [InlineData("11.11.11", "invalid startDatePersian")]
        [InlineData("11-123-3-1", "invalid startDatePersian")]
        public void ChairActivityValidator_CheckForStartDatePersian_ThrowsValditaionException(string persianDate
            , string exceptionMessage)
        {
            var activity = new ChairActivityBuilder().WithStartDatePersian(persianDate).Build();

            var result = _validator.TestValidate(activity);
            result.ShouldHaveValidationErrorFor(i => i.StartDatePersian).WithErrorMessage(exceptionMessage);
        }

        [Fact]
        public void DeactivateChair_CheckforInvalidChairId_ThrowsNotFoundException()
        {
            var activity = new ChairActivityBuilder().Build();

            void result() => _service.Deactivate(activity.ChairId, activity.Description, activity.StartDate
                , activity.AdminGuid, activity.AdminFullName);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("chair not found");
        }

        [Fact]
        public void DeactivateChair_CheckForInvaidAdminGuid_ThrowsNotFoundException()
        {
            var activity = new ChairActivityBuilder().Build();
            _chairFakeRepository.SetExistingId(activity.ChairId);

            void result() => _service.Deactivate(activity.ChairId, activity.Description, activity.StartDate
                , activity.AdminGuid, activity.AdminFullName);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("admin not found");
        }

        [Fact]
        public void DeactivateChair_CheckForValidation_ThrowsNotAcceptableException()
        {
            var activity = new ChairActivityBuilder().WithAdminFullName("").Build();
            _chairFakeRepository.SetExistingId(activity.ChairId);
            _adminFakeRepository.SetExistingGuid(activity.AdminGuid);

            void result() => _service.Deactivate(activity.ChairId, activity.Description, activity.StartDate
                , activity.AdminGuid, activity.AdminFullName);

            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void DeactivateListOfChairs_CheckForWorkingWell()
        {
            var activity = new ChairActivityBuilder().Build();
            _chairFakeRepository.SetExistingIdList(_chairIdList);
            _adminFakeRepository.SetExistingGuid(activity.AdminGuid);

            var result = _service.Deactivate(_chairIdList, activity.Description, activity.StartDate, activity.AdminGuid, activity.AdminFullName);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void DeactivateListOfChairs_CheckForInvalidAdminGuid_ThorwNotFoundException()
        {
            var activity = new ChairActivityBuilder().Build();

            void result() => _service.Deactivate(_chairIdList, activity.Description, activity.StartDate, activity.AdminGuid, activity.AdminFullName);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("admin not found");
        }

        [Fact]
        public void DeactivateListOfChairs_CheckForExistingAllChairId_ThrowNotFoundException()
        {
            var activity = new ChairActivityBuilder().Build();
            _adminFakeRepository.SetExistingGuid(activity.AdminGuid);
            _chairFakeRepository.SetExistingIdList(_chairIdList);
            var IdList = new List<int>() { 1, 2, 3, 4 };

            void result() => _service.Deactivate(IdList, activity.Description, activity.StartDate, activity.AdminGuid, activity.AdminFullName);

            var exception = Assert.Throws<NotFoundException>(result);
            exception.Message.Should().Be("chair not found");
        }

        [Fact]
        public void DeactivateListOfChair_CheckCountOfPassedChairActiviesAndAddedChairActivities()
        {
            var activity = new ChairActivityBuilder().Build();
            _adminFakeRepository.SetExistingGuid(activity.AdminGuid);
            _chairFakeRepository.SetExistingIdList(_chairIdList);

            var result = _service.Deactivate(_chairIdList, activity.Description, activity.StartDate, activity.AdminGuid, activity.AdminFullName);

            _chairActivityFakeRepository.Storage.Count.Should().Be(_chairIdList.Count);
        }
    }
}
