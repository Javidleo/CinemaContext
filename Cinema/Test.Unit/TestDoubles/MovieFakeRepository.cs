using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class MovieFakeRepository :BaseRepository<Movie>, IMovieRepository
    {
        private int _existingId;

        public MovieFakeRepository(ICinemaContext context) : base(context) { }

        public void SetExistingId(int id) => _existingId = id;

        public override void Add(Movie movie) { }

        public bool DoesExist(int id)
        {
            if (id == _existingId) return true;
            return false;
        }
    }
}