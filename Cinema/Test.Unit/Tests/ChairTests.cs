using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.ServiceContract;
using UseCases.Services;
using UseCases.Exceptions;
using Xunit;

namespace Test.Unit.Tests
{
    public class ChairTests
    {
        private readonly IChairService _chairService;
        private readonly ChairFakeRepository _chairFakeRepository;
        private readonly SalonFakeRepository _salonFakeRepository;

        public ChairTests()
        {
            _chairFakeRepository = new ChairFakeRepository();
            _salonFakeRepository = new SalonFakeRepository();
            _chairService = new ChairService(_chairFakeRepository, _salonFakeRepository);
        }

        [Fact]
        public void CreateChair_CheckForWorkingWell()
        {
            var chair = new ChairBuilder();
            _salonFakeRepository.SetExistingId(chair.salonId);

            var result = _chairService.Create(chair.row, chair.count, chair.salonId);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateChair_CheckForInvalidSalonId_ThorwsNotFoundException()
        {
            var chair = new ChairBuilder();

            void result() => _chairService.Create(chair.row, chair.count, chair.salonId);
            Assert.Throws<NotFoundException>(result);
        }


        
    }
}
