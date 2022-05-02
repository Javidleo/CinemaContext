using DomainModel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CinemaContext _context;
        public CustomerRepository()
        => _context = new CinemaContext();

        public void Add(Customer customer)
        {
            _context.Customer.Add(customer);
            _context.SaveChanges();
        }

        public Customer Find(int Id)
        => _context.Customer.FirstOrDefault(i=> i.Id == Id);

        public bool DoesExist(string email)
        => _context.Customer.Any(i => i.Email == email);

        public bool DoesExist(int? Id)
        => _context.Customer.Any(i => i.Id == Id);
    }
}
