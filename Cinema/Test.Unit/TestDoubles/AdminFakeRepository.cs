using DomainModel.Domain;
using System;
using Test.Unit.builders;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    internal class AdminFakeRepository : IAdminRepository
    {
        private int _existingid=-1; // we cannot to set Id for entities and entity id shoud be 0 as default so we pass -1
        private Guid _existingGuid = Guid.NewGuid();
        private string? _existingEmail;
        private string? _existingUserName;

        public void SetExistingId(int id) => _existingid = id;
        public void SetExistingGuid(Guid adminGuid)=> _existingGuid = adminGuid;
        public void SetExistingEmail(string email) => _existingEmail = email;
        public void SetExistingUserName(string userName) => _existingUserName = userName;

        public bool DoesExist(int id)
        {
            if (id == _existingid) return true;
            return false;
        }

        public bool DoesExist(Guid adminGuid)
        {
            if (adminGuid == _existingGuid) return true;
            return false;
        }

        public Admin FindByUserName(string userName)
        {
            if (userName == _existingUserName) return new AdminBuilder().Build();
            return null;
        }

        public Admin FindByEmail(string email)
        {
            if (email == _existingEmail) return new AdminBuilder().Build();
            return null;
        }

        public Admin Find(int Id)
        {
            if (Id == _existingid) return new AdminBuilder().Build();
            return null;
        }

        public Admin Find(Guid adminGuid)
        {
            if (adminGuid == _existingGuid) return new AdminBuilder().Build();
            return null;
        }
    }
}