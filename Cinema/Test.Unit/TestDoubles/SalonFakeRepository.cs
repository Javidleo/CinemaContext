using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class SalonFakeRepository : BaseRepository<Salon>, ISalonRepository
    {
        private int _existingId = -1;

        public SalonFakeRepository(ICinemaContext context) : base(context) { }

        public override void Add(Salon salon) { }

        public void SetExistingId(int id) => _existingId = id;

        public bool DoesExist(int Id)
        {
            if (Id == _existingId) return true;
            return false;
        }

        public Salon FindWithParents(int salonId)
        => throw new NotImplementedException(); // use mock insted

    }
}