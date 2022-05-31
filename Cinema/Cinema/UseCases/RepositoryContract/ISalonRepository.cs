using DomainModel.Domain;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface ISalonRepository : IBaseRepository<Salon>
    {
        bool DoesExist(int Id);
        // include salon parents two level deep to find cienam activity status
        Salon FindWithParents(int salonId);
    }
}