using DomainModel.Domain;

namespace Test.Unit.builders
{
    internal class ChairBuilder
    {
        public int salonId  = 1;
        public int count = 1;
        public byte row  = 1;
        public byte number  = 1;

        public ChairBuilder WithSalonId(int salonId)
        {
            this.salonId = salonId;
            return this;
        }
        public ChairBuilder WithNumber(byte number)
        {
            this.number = number;
            return this;
        }
        public ChairBuilder WithCount(int count)
        {
            this.count = count;
            return this;
        }
        public ChairBuilder WithRow(byte row)
        {
            this.row = row;
            return this;
        }
        public Chair Build()
        => Chair.Create(salonId, number, row);
    }
}
