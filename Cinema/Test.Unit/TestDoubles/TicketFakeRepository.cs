using DomainModel.Domain;
using System.Collections.Generic;
using UseCases.RepositoryContract;


namespace Test.Unit.TestDoubles
{
    internal class TicketFakeRepository : ITicketRepository
    {
        private int _existingId;
        public List<Ticket> storage = new List<Ticket>(); 
        public void SetExistingId(int id)=> _existingId = id;

        public void Add(Ticket ticket)
        {
            storage.Add(ticket);
        }

        public bool DoesExist(int id)
        {
            if (id == _existingId) return true;
            return false;
        }
    }
}