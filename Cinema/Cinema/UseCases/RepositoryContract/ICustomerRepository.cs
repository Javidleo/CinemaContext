using DomainModel.Domain;

namespace UseCases.RepositoryContract
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        Customer Find(int Id);
        bool DoesExist(string email);
        bool DoesExist(int? id);
    }
}