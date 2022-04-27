using DomainModel.Domain;

namespace Test.Unit.builders
{
    internal class ChairBuilder
    {
        public int _salonId = 1;
        public int _count = 1;
        public byte _row = 1;
        public byte _number = 1;

        public ChairBuilder WithSalonId(int salonId)
        {
            _salonId = salonId;
            return this;
        }
        public ChairBuilder WithNumber(byte number)
        {
            _number = number;
            return this;
        }
        public ChairBuilder WithCount(int count)
        {
            _count = count;
            return this;
        }
        public ChairBuilder WithRow(byte row)
        {
            _row = row;
            return this;
        }
        public Chair Build()
        => Chair.Create(_salonId, _number, _row);
    }
}
