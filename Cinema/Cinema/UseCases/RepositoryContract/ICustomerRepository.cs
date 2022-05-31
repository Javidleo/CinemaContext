using DomainModel.Domain;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface ICustomerRepository:IBaseRepository<Customer>
    {
        bool DoesExist(string email);
        bool DoesExist(int? id);
    }
}