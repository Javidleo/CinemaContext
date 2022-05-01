using DomainModel.Domain;
using System;
using System.Threading.Tasks;

namespace UseCases.ServiceContract
{
    public interface IAdminService
    {
        Task Create(int cinemaId, string name, string family, string nationalCode, string email,string userName, string password);
        Task<Admin> Find(string key,string password);
        Task Modify(Guid adminGuid, string name, string family, string email, string userName);
        Task ChangePasword(Guid adminGuid, string password);
    }
}
