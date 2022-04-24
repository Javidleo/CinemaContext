using DomainModel.Domain;

namespace UseCases.RepositoryContract
{
    public interface IMovieRepository
    {
        void Add(Movie movie);
        bool DoesExist(int id);
    }
}