using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class SnasFakeRepository : BaseRepository<Sans>, ISansRepository
    {
        private int _existingId;

        public SnasFakeRepository(ICinemaContext context) : base(context) { }

        public void SetExistingId(int id) => _existingId = id;

        public bool DoesExist(int id)
        {
            if (id == _existingId) return true;
            return false;
        }
    }
}