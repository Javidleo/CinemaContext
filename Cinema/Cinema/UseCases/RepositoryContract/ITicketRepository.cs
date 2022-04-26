using DomainModel.Domain;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface ITicketRepository
    {
        void Add(List<Ticket> ticketList);
        void Add(Ticket ticket);
        bool DoesExist(int id);
    }
}