using DomainModel.Domain;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaContext _context;
        public TicketRepository(CinemaContext context)
        => _context = context;

        public void Add(Ticket ticket)
        {
            _context.Ticket.Add(ticket);
            _context.SaveChanges();
        }

        public bool DoesExist(int id)
        => _context.Ticket.Any(i => i.Id == id);
    }
}
