using DomainModel.Domain;

namespace UseCases.RepositoryContract
{
    public interface ISalonActivityRepository
    {
        void Add(SalonActivity salonActivity);
    }
}