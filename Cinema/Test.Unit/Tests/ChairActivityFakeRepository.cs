using DomainModel.Domain;
using System.Collections.Generic;

namespace Test.Unit.Tests
{
    public class ChairActivityFakeRepository : IChairactivityRepository
    {
        public List<ChairActivity> Storage = new();
        public void Add(ChairActivity chairActivity)
        {
            Storage.Add(chairActivity); 
        }

        public void Add(List<ChairActivity> chairActivityList)
        {
            Storage.AddRange(chairActivityList);
        }
    }
}