using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class CinemaRepository : BaseRepository<Cinema>, ICinemaRepository
    {
        private readonly ICinemaContext _context;
        public CinemaRepository(ICinemaContext context) : base(context)
        => _context = context;

        public bool DoesExist(int Id)
        => _context.Cinema.Any(i => i.Id == Id);
    }
}
