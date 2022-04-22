using DomainModel;
using System.Collections.Generic;
using Test.Unit.builders;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class ChairFakeRepository : IChairRepository
    {
        private int _existingId;

        public List<Chair> Storage = new List<Chair>();

        public void SetExistingId(int id) => _existingId = id;

        public Chair FindWithParents(int id)
        {
            if (_existingId == id) return new ChairBuilder().Build();
            return null;
        }

        public bool DoesExist(int id)
        {
            if (id == _existingId) return true;
            return false;
        }

        public void Add(List<Chair> chairs)
        {
            Storage.AddRange(chairs);
        }
    }

}
