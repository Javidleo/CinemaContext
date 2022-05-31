using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class CinemaFakeRepository : BaseRepository<Cinema>, ICinemaRepository
    {
        private int _existingId = -1;

        public CinemaFakeRepository(ICinemaContext context) : base(context) { }

        public void SetExistingId(int id) => _existingId = id;

        public bool DoesExist(int Id)
        {
            if (Id == _existingId) return true;
            return false;
        }
    }
}