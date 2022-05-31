using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class SansRepository : BaseRepository<Sans>, ISansRepository
    {
        private readonly ICinemaContext _context;
        public SansRepository(ICinemaContext context) : base(context)
        => _context = context;

        public bool DoesExist(int id)
        => _context.Sans.Any(i => i.Id == id);
    }
}
