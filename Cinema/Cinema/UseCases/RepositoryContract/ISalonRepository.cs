using DomainModel.Domain;

namespace UseCases.RepositoryContract
{
    public interface ISalonRepository
    {
        bool DoesExist(int Id);
        void Add(Salon salon);

        // include salon parents two level deep to find cienam activity status
        Salon FindWithParents(int salonId);
    }
}