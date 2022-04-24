using DomainModel.Domain;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaContext _context;
        public MovieRepository()
        => _context = new CinemaContext();

        public void Add(Movie movie)
        {
            _context.Movie.Add(movie);
            _context.SaveChanges();
        }

        public bool DoesExist(int id)
        => _context.Movie.Any(i => i.Id == id);
    }
}
