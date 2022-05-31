using DomainModel.Domain;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface ICinemaRepository:IBaseRepository<Cinema>
    {
        bool DoesExist(int Id);
    }
}