namespace Application.DTO;

public class CreateTicketDTO
{
    public int? CustomerId { get; set; }
    public int ChairId { get; set; }
    public decimal Price { get; set; }
}
