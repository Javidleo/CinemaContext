using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class SansRepository : ISansRepository
    {
        private readonly CinemaContext _context;
        public SansRepository()
        => _context = new CinemaContext();

        public bool DoesExist(int id)
        => _context.Sans.Any(i => i.Id == id);
    }
}
