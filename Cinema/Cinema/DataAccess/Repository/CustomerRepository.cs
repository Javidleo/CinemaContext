using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly ICinemaContext _context;
        public CustomerRepository(ICinemaContext context) : base(context)
        => _context = context;

        public bool DoesExist(string email)
        => _context.Customer.Any(i => i.Email == email);

        public bool DoesExist(int? Id)
        => _context.Customer.Any(i => i.Id == Id);
    }
}
