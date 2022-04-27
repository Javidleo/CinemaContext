using DomainModel.Domain;

namespace Test.Unit.builders
{
    internal class TicketBuilder
    {
        private int _customerId = 1;
        private int _chairId = 1;
        private int _salonId = 1;
        private int _cinemaId = 1;
        private decimal _price = 1000;
        private int _movieSansSalonId = 1;

        public TicketBuilder withCustomerId(int customerId)
        {
            _customerId = customerId;
            return this;
        }
        public TicketBuilder WithChairId(int chairId)
        {
            _chairId = chairId;
            return this;
        }
        public TicketBuilder WithSalonId(int salonId)
        {
            _salonId = salonId;
            return this;
        }
        public TicketBuilder WithCinemaId(int cinemaId)
        {
            _cinemaId = cinemaId;
            return this;
        }
        public TicketBuilder WithMovieSansSalonId(int movieSansSalonId)
        {
            _movieSansSalonId = movieSansSalonId;
            return this;
        }
        public TicketBuilder WithPrice(decimal price)
        {
            _price = price;
            return this;
        }

        public Ticket Build()
        => Ticket.Create(_customerId, _chairId, _salonId, _cinemaId, _movieSansSalonId, _price);
    }
}