using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class SnasFakeRepository : ISansRepository
    {
        private int _existingId;

        public void SetExistingId(int id) => _existingId = id;

        public bool DoesExist(int id)
        {
            if (id == _existingId) return true;
            return false;
        }
    }
}