using DomainModel;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class MovieSansSalonRepository : IMovieSansSalonRepository
    {
        private readonly CinemaContext _context;
        public MovieSansSalonRepository()
        => _context = new CinemaContext();

        public void Add(MovieSansSalon obj)
        {
            _context.MovieSansSalon.Add(obj);
            _context.SaveChanges();
        }
    }
}
