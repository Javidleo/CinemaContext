using DomainModel.Domain;
using System;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface IAdminRepository:IBaseRepository<Admin>
    {
        bool DoesExist(int id);
        bool DoesExist(Guid adminGuid);
        Admin Find(Guid adminGuid);
        Admin FindByUserName(string userName);
        Admin FindByEmail(string email);
    }
}