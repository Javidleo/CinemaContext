using DomainModel.Domain;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class MovieFakeRepository : IMovieRepository
    {
        private int _existingId;

        public void SetExistingId(int id)=> _existingId = id;

        public void Add(Movie movie)
        {

        }

        public bool DoesExist(int id)
        {
            if (id == _existingId) return true;
            return false;
        }
    }
}