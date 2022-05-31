using DomainModel.Domain;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        bool DoesExist(int id);
    }
}