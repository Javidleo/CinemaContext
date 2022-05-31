using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    public class CinemaActivityFakeRepository : BaseRepository<CinemaActivity>, ICinemaActivityRepository
    {
        public CinemaActivityFakeRepository(ICinemaContext context) : base(context) { }

        public override void Add(CinemaActivity cinemaActivity)
        {

        }
    }
}