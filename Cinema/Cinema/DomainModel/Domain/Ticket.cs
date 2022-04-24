using System;

namespace DomainModel.Domain
{
    public class Ticket
    {
        public int Id { get; private set; }

        public int? CustomerId { get; private set; }

        public int ChairId { get; private set; }

        public int SalonId { get; private set; }

        public int CinemaId { get; private set; }

        public decimal Price { get; private set; }

        public Guid TicketGuid { get; private set; }

        public virtual Chair Chair { get; private set; }

        public virtual Salon Salon { get; private set; }

        public virtual Cinema Cinema { get; private set; }

        public virtual Customer Customer { get; private set; }

        private Ticket(int? customerId, int chairId, int salonId, int cinemaId, decimal price)
        {
            CustomerId = customerId;
            ChairId = chairId;
            SalonId = salonId;
            CinemaId = cinemaId;
            Price = price;
        }

        public static Ticket Create(int? customerId, int chairId, int salonId, int cinemaId, decimal price)
        => new(customerId, chairId, salonId, cinemaId, price);
    }
}
