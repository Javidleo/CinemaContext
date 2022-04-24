using DomainModel.Domain;

namespace UseCases.RepositoryContract
{
    public interface IAdminRepository
    {
        bool DoesExist(int id);
        Admin FindByUserName(string userName);
        Admin FindByEmail(string email);
    }
}