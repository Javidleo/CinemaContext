using DomainModel.Domain;

namespace UseCases.RepositoryContract
{
    public interface ICinemaActivityRepository
    {
        void Add(CinemaActivity cinemaActivity);
    }
}