using DomainModel;

namespace UseCases.RepositoryContract
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        bool DoesExist(string email);
        bool DoesExist(int? id);
    }
}