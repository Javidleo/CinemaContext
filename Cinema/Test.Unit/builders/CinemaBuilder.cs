using DomainModel.Domain;
using System;

namespace Test.Unit.builders
{
    internal class CinemaBuilder
    {
        private string _name = "cinema";
        private decimal _ticketPrice = 30000;
        private string _address = "new state south";
        private int _cityId = 1;

        public CinemaBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public CinemaBuilder WithTicketPrice(decimal price)
        {
            _ticketPrice = price;
            return this;
        }
        public CinemaBuilder WithAddress(string address)
        {
            _address = address;
            return this;
        }
        public CinemaBuilder WithCityId(int cityId)
        {
            _cityId = cityId;
            return this;
        }

        public Cinema Build()
        => Cinema.Create(_name, _ticketPrice, _address, _cityId);
    }
}