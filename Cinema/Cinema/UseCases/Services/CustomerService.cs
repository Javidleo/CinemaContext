using DomainModel;
using DomainModel.Validation;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerValidator _validator;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _validator = new CustomerValidator();
        }
        public Task Create(string name, string family, string email)
        {
            var customer = Customer.Create(name, family, email);

            if (!_validator.Validate(customer).IsValid)
                throw new NotAcceptableException("invalid input values");

            if (_customerRepository.DoesExist(email))
                throw new ConflictException("duplicate email");

            return Task.CompletedTask;
        }
    }
}