using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.ServiceContract;
using UseCases.Services;
using Xunit;

namespace Test.Unit.Tests
{
    public class SalonTests
    {
        private readonly ISalonService _salonService;
        private readonly SalonValidator _validator;
        private readonly CinemaFakeRepository _cinemaFakeRepository;
        private readonly SalonFakeRepository _salonFakeRepository;
        public SalonTests()
        {
            _validator = new SalonValidator();
            _cinemaFakeRepository = new CinemaFakeRepository();
            _salonFakeRepository = new SalonFakeRepository();
            _salonService = new SalonService(_cinemaFakeRepository, _salonFakeRepository);
        }

        [Theory]
        [InlineData("", "name is empty")]
        [InlineData("f@#@", "invalid name")]
        [InlineData("EQWR@ ! !$ ", "invalid name")]
        public void SalonValidation_CheckForName_ThorwValidationException(string name, string exceptionMessage)
        {
            var salon = new SalonBuilder().WithName(name).Build();

            var result = _validator.TestValidate(salon);
            result.ShouldHaveValidationErrorFor(i => i.Name).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50000)]
        [InlineData(0)]
        [InlineData(4.5)]
        public void SalonValidation_CheckForCapacity_ThrowValidationException(int capacity)
        {
            var salon = new SalonBuilder().WithCapacity(capacity).Build();

            var result = _validator.TestValidate(salon);
            result.ShouldHaveValidationErrorFor(i => i.Capacity).WithErrorMessage("invalid capacity");
        }

        [Fact]
        public void CreateSalon_CheckForWorkingWell()
        {
            var salon = new SalonBuilder().Build();
            _cinemaFakeRepository.SetExistingId(salon.CinemaId);

            var result = _salonService.Create(salon.CinemaId, salon.Name, salon.Capacity);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateSalon_CheckForNonExistingCinemaId_ThrowNotFoundException()
        {
            var salon = new SalonBuilder().Build();

            void result() => _salonService.Create(salon.CinemaId, salon.Name, salon.Capacity);
            Assert.Throws<NotFoundException>(result);
        }

        [Theory]
        [InlineData("",50)]
        [InlineData("name",0)]
        [InlineData("name",4)]
        [InlineData("name",5000)]
        [InlineData("%sname",5000)]
        public void CreateSalon_CheckForSalonValidation_ThrowNotAcceptableException(string name,int capacity)
        {
            var salon = new SalonBuilder().WithName(name).WithCapacity(capacity).Build();
            _cinemaFakeRepository.SetExistingId(salon.CinemaId);

            void result() => _salonService.Create(salon.CinemaId, salon.Name, salon.Capacity);
            Assert.Throws<NotAcceptableException>(result);
        }
    }
}
