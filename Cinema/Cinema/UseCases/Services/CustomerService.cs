using DomainModel.Domain;
using DomainModel.Validation;
using FluentValidation;
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

        public Task Create(string name, string family, string email, string password)
        {
            if (_customerRepository.DoesExist(email))
                throw new ConflictException("duplicate email");

            var customer = Customer.Create(name, family, email, password);

            if (!_validator.Validate(customer, option => option.IncludeRuleSets("*")).IsValid)
                throw new NotAcceptableException("invalid input values");


            return Task.CompletedTask;
        }

        public Task Modify(int Id, string name, string family, string email)
        {
            var customer = _customerRepository.Find(Id);
            if (customer is null)
                throw new NotFoundException("custoemr not found");

            customer.Modify(name, family, email);

            if (!_validator.Validate(customer, option => option.IncludeRuleSets("Name", "Email")).IsValid)
                throw new NotAcceptableException("invalid customer");

            return Task.CompletedTask;
        }

        public Task ChangePassword(int id, string password)
        {
            var customer = _customerRepository.Find(id);
            if (customer is null)
                throw new NotFoundException("customer not found");

            customer.ChangePassword(password);

            if (!_validator.Validate(customer, option => option.IncludeRuleSets("Password")).IsValid)
                throw new NotAcceptableException("invalid customer");

            return Task.CompletedTask;
        }
    }
}