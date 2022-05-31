using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System.Collections.Generic;
using UseCases.RepositoryContract;


namespace Test.Unit.TestDoubles
{
    internal class TicketFakeRepository : BaseRepository<Ticket>, ITicketRepository
    {
        private int _existingId;
        public List<Ticket> storage = new List<Ticket>();

        public TicketFakeRepository(ICinemaContext context) : base(context) { }

        public void SetExistingId(int id) => _existingId = id;

        public override void Add(Ticket ticket)
        {
            storage.Add(ticket);
        }

        public override void Add(List<Ticket> ticketList)
        {
            storage.AddRange(ticketList);
        }

        public bool DoesExist(int id)
        {
            if (id == _existingId) return true;
            return false;
        }


    }
}