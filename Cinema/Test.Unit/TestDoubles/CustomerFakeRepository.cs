using DomainModel.Domain;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class CustomerFakeRepository : ICustomerRepository
    {
        private int _existingId;
        private string? _existingEmail;

        public void SetExistingId(int id)=> _existingId = id;
        public void SetExistingEmail(string email) => _existingEmail = email;

        public void Add(Customer customer) { }

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
    }
}
