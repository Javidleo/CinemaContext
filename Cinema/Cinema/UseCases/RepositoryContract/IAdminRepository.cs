using DomainModel.Domain;
using System;

namespace UseCases.RepositoryContract
{
    public interface IAdminRepository
    {
        bool DoesExist(int id);
        bool DoesExist(Guid adminGuid);
        Admin Find(int Id);
        Admin Find(Guid adminGuid);
        Admin FindByUserName(string userName);
        Admin FindByEmail(string email);
    }
}