using DomainModel.Domain;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface ISansRepository: IBaseRepository<Sans>
    {
        bool DoesExist(int id);

    }
}