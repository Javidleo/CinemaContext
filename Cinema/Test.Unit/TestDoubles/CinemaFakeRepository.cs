using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class CinemaFakeRepository : ICinemaRepository
    {
        private int _existingId=-1;
        public void SetExistingId(int id)=> _existingId = id;

        public bool DoesExist(int Id)
        {
            if (Id == _existingId) return true;
            return false;
        }
    }
}