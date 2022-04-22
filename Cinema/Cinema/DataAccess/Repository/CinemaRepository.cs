using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly CinemaContext _context;
        public CinemaRepository()
        => _context = new CinemaContext();

        public bool DoesExist(int Id)
        => _context.Cinema.Any(i => i.Id == Id);
    }
}
