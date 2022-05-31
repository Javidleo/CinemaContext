using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class SalonActivityFakeRepository : BaseRepository<SalonActivity>, ISalonActivityRepository
    {
        public SalonActivityFakeRepository(ICinemaContext context) : base(context) { }

        public override void Add(SalonActivity salonActivity)
        {

        }
    }
}