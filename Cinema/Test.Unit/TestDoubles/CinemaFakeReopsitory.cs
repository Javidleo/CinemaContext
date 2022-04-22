using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class CinemaFakeReopsitory : ICinemaRepository
    {
        private int _existingId;
        public void SetExistingId(int id)=> _existingId = id;

        public bool DoesExist(int Id)
        {
            if (Id == _existingId) return true;
            return false;
        }
    }
}