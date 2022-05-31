using DomainModel.Domain;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        bool DoesExist(int id);
    }
}