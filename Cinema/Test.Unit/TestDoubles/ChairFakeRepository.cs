using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class ChairFakeRepository : BaseRepository<Chair>, IChairRepository
    {
        private int _existingId = -1;
        public List<Chair> Storage = new();
        private List<int> _existingIdList = new();

        public ChairFakeRepository(ICinemaContext context) : base(context) { }

        public void SetExistingId(int id) => _existingId = id;
        public void SetExistingIdList(List<int> IdList) => _existingIdList = IdList;
        public override void Add(List<Chair> chairs)
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
        public bool DoesExist(List<int> chairIdList)
        {
            if (chairIdList.Equals(_existingIdList)) return true;
            return false;
        }
        public List<Chair> FindBySalon(int salonId)
        => throw new NotImplementedException(); // use mock insted
    }

}
