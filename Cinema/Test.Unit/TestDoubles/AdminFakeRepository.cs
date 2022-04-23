using DomainModel;
using Test.Unit.builders;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class AdminFakeRepository : IAdminRepository
    {
        private int _existingid;
        private string? _existingEmail;
        private string? _existingUserName;

        public void SetExistingId(int id) => _existingid = id;
        public void SetExistingEmail(string email) => _existingEmail = email;
        public void SetExistingUserName(string userName)=> _existingUserName = userName;


        public bool DoesExist(int id)
        {
            if (id == _existingid) return true;
            return false;
        }

        public Admin FindByUserName(string userName)
        {
            if (userName == _existingUserName) return new AdminBuilder().Build();
            return null;
        }

        public Admin FindByEmail(string email)
        {
            if(email == _existingEmail) return new AdminBuilder().Build();
            return null;
        }
    }
}