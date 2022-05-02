using DomainModel.Domain;
using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.ServiceContract;
using UseCases.Services;
using Xunit;

namespace Test.Unit.Tests
{
    public class CustomerTests
    {
        private readonly ICustomerService _service;
        private readonly CustomerValidator _validator;
        private readonly Customer _customer;
        private readonly CustomerFakeRepository _customerFakeRepository;
        public CustomerTests()
        {
            _customerFakeRepository = new CustomerFakeRepository();
            _service = new CustomerService(_customerFakeRepository);
            _validator = new CustomerValidator();
            _customer = new CustomerBuilder().Build();
        }

        [Fact]
        public void CreateCustomer_CheckForWorkingWell()
        {
            var customer = new CustomerBuilder().Build();

            var result = _service.Create(customer.Name, customer.Family, customer.Email, customer.Password);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory]
        [InlineData("","name is empty")]
        [InlineData("f234!", "invalid name")]
        [InlineData("f234", "invalid name")]
        [InlineData("nfds2", "invalid name")]
        public void CustomerValidation_CheckForInvaildName_ThrowValidationError(string name,string exceptionMessage)
        {
            var customer = new CustomerBuilder().WithName(name).Build();
            var result = _validator.TestValidate(customer, option => option.IncludeRuleSets("Name"));
            result.ShouldHaveValidationErrorFor(i => i.Name).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData("","family is empty")]
        [InlineData("javid21354","invalid family")]
        [InlineData("javi@", "invalid family")]
        [InlineData("رضتا!","invalid family")]
        public void CustomerValidation_CheckForInvalidFamily_ThrowValidationError(string family,string exceptionMessage)
        {
            var customer = new CustomerBuilder().WithFamily(family).Build();
            var result = _validator.TestValidate(customer,option=> option.IncludeRuleSets("Name"));

            result.ShouldHaveValidationErrorFor(i => i.Family).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData("","email is empty")]
        [InlineData("javid1`5r23@","invalid email")]
        public void CustoemrValidation_CheckForInvalidEmail_ThrowsValidationError(string email,string exceptionMessage)
        {
            var customer = new CustomerBuilder().WithEmail(email).Build();
            var result = _validator.TestValidate(customer,option=> option.IncludeRuleSets("Email"));

            result.ShouldHaveValidationErrorFor(i => i.Email).WithErrorMessage(exceptionMessage);
        }

       
        [Fact]
        public void CreateCustomer_CheckForInvalidData_ThrowsNotAcceptableException()
        {
            var customer = new CustomerBuilder().WithPassword("f").Build();
            void result() => _service.Create(customer.Name,customer.Family,customer.Email,customer.Password);

            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void CreateCustomer_CheckForDuplicateEmailAddress_ThrowsDuplicateException()
        {
            _customerFakeRepository.SetExistingEmail(_customer.Email);

            void result() => _service.Create(_customer.Name, _customer.Family, _customer.Email,_customer.Password);
            Assert.Throws<ConflictException>(result);
        }

        [Fact]
        public void ModifyCustoemr_CheckForWorkingWell()
        {
            _customerFakeRepository.SetExistingId(_customer.Id);
            var result = _service.Modify(_customer.Id,_customer.Name, _customer.Family, _customer.Email);


            result.Status.ToString().Should().Be("RanToCompletion");
        }
        
        [Fact]
        public void ModifyCustomer_CheckForInvalidCustomerId_ThrowNotFoundException()
        {
            void result() => _service.Modify(_customer.Id, _customer.Name, _customer.Family, _customer.Email);
            
            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void ModifyCustomer_CheckForValidation_ThrowNotAcceptableException()
        {
            _customerFakeRepository.SetExistingId(_customer.Id);
            _customer.Modify("", "family", "email"); // we modify the customer to be invalid;

            void result() => _service.Modify(_customer.Id, _customer.Name, _customer.Family, _customer.Email);
            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void ChangeCustomerPassword_CheckForWorkingWell()
        {
            _customerFakeRepository.SetExistingId(_customer.Id);
            var result = _service.ChangePassword(_customer.Id, _customer.Password);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void ChangeCustoemrPassword_CheckForInvalidCustomerId_ThrowNotFoundException()
        {
            void result() => _service.ChangePassword(_customer.Id, _customer.Password);

            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void ChangeCustomerPassword_CehckForValidation_ThrowNotAcceptableException()
        {
            _customer.ChangePassword("new");// pasword must be more than 8 characters and contain one or more upper case
            _customerFakeRepository.SetExistingId(_customer.Id);

            void result() => _service.ChangePassword(_customer.Id, _customer.Password);

            Assert.Throws<NotAcceptableException>(result);
        }
    }
}
