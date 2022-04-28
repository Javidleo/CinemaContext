namespace WebApplicationMVC.DTO
{
    public class CreateTicketDTO
    {
        public int? CustomerId { get; set; }

        public int CinemaId { get; set; }
        public int SalonId { get; set; }
        public int Count { get; set; }
        public int MovieSansSalonId { get; set; }
        public decimal  TicketPrice { get; set; }
    }
}
