using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using Test.Unit.builders;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class CustomerFakeRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private int _existingId = -1;
        private string? _existingEmail;

        public CustomerFakeRepository(ICinemaContext context) : base(context) { }

        public void SetExistingId(int id) => _existingId = id;
        public void SetExistingEmail(string email) => _existingEmail = email;

        public override void Add(Customer customer) { }

        public bool DoesExist(string email)
        {
            if (email == _existingEmail) return true;
            return false;
        }

        public bool DoesExist(int? id)
        {
            if (id == _existingId) return true;
            return false;
        }

        public override Customer Find(int Id)
        {
            if (Id == _existingId) return new CustomerBuilder().Build();
            return null;
        }
    }
}
