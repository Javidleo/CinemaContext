using DomainModel.Domain;
using Test.Unit.builders;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class SalonFakeRepository : ISalonRepository
    {
        private int _existingId=-1;

        public void Add(Salon salon)
        {

        }

        public void SetExistingId(int id)=> _existingId = id;

        public bool DoesExist(int Id)
        {
            if (Id == _existingId) return true;
            return false;
        }

        public Salon FindWithParents(int salonId)
        {
            if (_existingId == salonId) return new SalonBuilder().Build();
            return null;
        }
    }
}