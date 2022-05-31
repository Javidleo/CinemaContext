using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    public class ChairActivityFakeRepository : BaseRepository<ChairActivity>,IChairactivityRepository
    {
        public List<ChairActivity> Storage = new();

        public ChairActivityFakeRepository(ICinemaContext context) : base(context)
        {
        }
        public override void Add(ChairActivity chairActivity)
        {
            Storage.Add(chairActivity);
        }

        public override void Add(List<ChairActivity> chairActivityList)
        {
            Storage.AddRange(chairActivityList);
        }
    }
}