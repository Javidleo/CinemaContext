using DomainModel.Domain;

namespace Test.Unit.builders
{
    internal class SalonBuilder
    {
        private int _cinemaId = 1;
        private string _name = "aftab";
        private int _capacity = 100;

        public SalonBuilder WithCinemaId(int id)
        {
            _cinemaId = id;
            return this;
        }
        public SalonBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public SalonBuilder WithCapacity(int capacity)
        {
            _capacity = capacity;
            return this;
        }

        public Salon Build()
        => Salon.Create(_cinemaId, _name, _capacity);
    }
}