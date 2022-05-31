using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        private readonly ICinemaContext _context;
        public MovieRepository(ICinemaContext context) : base(context)
        => _context = context;

        public bool DoesExist(int id)
        => _context.Movie.Any(i => i.Id == id);
    }
}
