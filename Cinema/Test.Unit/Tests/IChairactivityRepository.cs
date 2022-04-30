using DomainModel.Domain;
using System.Collections.Generic;

namespace Test.Unit.Tests
{
    public interface IChairactivityRepository
    {
        void Add(ChairActivity chairActivity);
        void Add(List<ChairActivity> chairActivityList);
    }
}