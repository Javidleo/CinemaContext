using DomainModel;

namespace UseCases.RepositoryContract
{
    public interface ITicketRepository
    {
        void Add(Ticket ticket);
        bool DoesExist(int id);
    }
}