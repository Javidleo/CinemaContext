using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        private readonly ICinemaContext _context;
        public TicketRepository(ICinemaContext context) : base(context)
        => _context = context;

        public bool DoesExist(int id)
        => _context.Ticket.Any(i => i.Id == id);
    }
}
