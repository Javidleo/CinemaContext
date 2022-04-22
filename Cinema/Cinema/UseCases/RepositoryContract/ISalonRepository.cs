using DomainModel;

namespace UseCases.RepositoryContract
{
    public interface ISalonRepository
    {
        bool DoesExist(int Id);
        void Add(Salon salon);
    }
}