using FluentAssertions;
using System.Linq;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.Services;
using Xunit;

namespace Test.Unit.Tests
{
    public class ChairTests
    {
        private readonly ChairService _chairService;
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
            var chair = new ChairBuilder();// dont use Build method because it just give us row, number and salonId
            _salonFakeRepository.SetExistingId(chair._salonId);

            var result = _chairService.Create(chair._row, chair._count, chair._salonId);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void CreateChair_CheckForInvalidSalonId_ThorwsNotFoundException()
        {
            var chair = new ChairBuilder();

            void result() => _chairService.Create(chair._row, chair._count, chair._salonId);
            Assert.Throws<NotFoundException>(result);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(2, 4)]
        [InlineData(3, 4)]
        [InlineData(3, 6)]
        [InlineData(2, 5)]
        [InlineData(6, 3)]
        public void CreateChair_CheckForCurrectCountOfCreatedChairs(int testRow1, int testRow2)
        {
            var chair = new ChairBuilder().WithCount(8).WithRow(6);
            _salonFakeRepository.SetExistingId(chair._salonId);


            _chairService.Create(chair._row, chair._count, chair._salonId);

            var result1 = _chairFakeRepository.Storage
                .Where(i => i.Number >= 1 && i.Number <= chair._number && i.Row == testRow1).ToList();

            var result2 = _chairFakeRepository.Storage
                .Where(i => i.Number >= 1 && i.Number <= chair._number && i.Row == testRow2).ToList();

            result1.Count.Should().Be(result2.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CreateChair_CheckForCorrectCreatedChairNumbers(int testRow)
        {
            var chair = new ChairBuilder().WithCount(5).WithRow(3);
            _salonFakeRepository.SetExistingId(chair._salonId);

            var serviceResult = _chairService.Create(chair._row, chair._count, chair._salonId);

            var result = _chairFakeRepository.Storage.Where(i => i.Row == testRow).ToList();

            for (byte i = 1; i <= chair._count; i++)// im not sure its a good idea to use loops here but , I use it this time
            {
                result.FirstOrDefault(c => c.Number == i).Should().NotBeNull();
            }

        }
    }
}
