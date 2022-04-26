using DomainModel.Domain;

namespace UseCases.RepositoryContract
{
    public interface ICinemaRepository
    {
        bool DoesExist(int Id);
    }
}