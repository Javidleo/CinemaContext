using DomainModel.Domain;
using System;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class ChairFakeRepository : IChairRepository
    {
        private int _existingId;
        public List<Chair> Storage = new List<Chair>();

        public void SetExistingId(int id) => _existingId = id;

        public void Add(List<Chair> chairs)
        {
            Storage.AddRange(chairs);
        }

        public Chair FindWithParents(int id)
        {
            throw new NotImplementedException(); // use mock insted of this method 
        }

        public bool DoesExist(int id)
        {
            if (id == _existingId) return true;
            return false;
        }

      

        public List<Chair> FindBySalon(int salonId)
        => throw new NotImplementedException(); // use mock insted
    }

}
