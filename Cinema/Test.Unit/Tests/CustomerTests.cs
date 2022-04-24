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
        private readonly ICustomerService _customerService;
        private readonly CustomerValidator _validator;
        private readonly Customer _customer;
        private readonly CustomerFakeRepository _customerFakeRepository;
        public CustomerTests()
        {
            _customerFakeRepository = new CustomerFakeRepository();
            _customerService = new CustomerService(_customerFakeRepository);
            _validator = new CustomerValidator();
            _customer = new CustomerBuilder().Build();
        }

        [Fact]
        public void CustomerValidation_CheckForNullName_ThrowValidationError()
        {
            var customer = new CustomerBuilder().WithName("").Build();
            var result = _validator.TestValidate(customer);

            result.ShouldHaveValidationErrorFor(i => i.Name).WithErrorMessage("name is empty");
        }

        [Theory]
        [InlineData("name23")]
        [InlineData("@$name")]
        [InlineData("رضا$")]
        [InlineData("رضا$123")]
        public void CustomerValidation_CheckForInvaildName_ThrowValidationError(string name)
        {
            var customer = new CustomerBuilder().WithName(name).Build();
            var result = _validator.TestValidate(customer);
            result.ShouldHaveValidationErrorFor(i => i.Name).WithErrorMessage("invalid name");
        }

        [Fact]
        public void CustomerValidation_CheckForNullFamily_ThrowValidationError()
        {
            var customer = new CustomerBuilder().WithFamily("").Build();
            var result = _validator.TestValidate(customer);

            result.ShouldHaveValidationErrorFor(i => i.Family).WithErrorMessage("family is empty");
        }

        [Theory]
        [InlineData("javid21354")]
        [InlineData("javi@")]
        [InlineData("رضتا!")]
        public void CustomerValidation_CheckForInvalidName_ThrowValidationError(string family)
        {
            var customer = new CustomerBuilder().WithFamily(family).Build();
            var result = _validator.TestValidate(customer);

            result.ShouldHaveValidationErrorFor(i => i.Family).WithErrorMessage("invalid family");
        }

        [Fact]
        public void CustomerValidation_CheckForNullEmail_throwValidationError()
        {
            var customer = new CustomerBuilder().WithEmail("").Build();
            var result = _validator.TestValidate(customer);

            result.ShouldHaveValidationErrorFor(i => i.Email).WithErrorMessage("email is empty");
        }

        [Fact]
        public void CustoemrValidation_CheckForInvalidEmail_ThrowsValidationError()
        {
            var customer = new CustomerBuilder().WithEmail("javi3%!.com").Build();
            var result = _validator.TestValidate(customer);

            result.ShouldHaveValidationErrorFor(i => i.Email).WithErrorMessage("invalid email");
        }

        [Fact]
        public void CreateCustomer_CheckForWorkingWell()
        {
            var customer = new CustomerBuilder().Build();

            var result = _customerService.Create(customer.Name, customer.Family, customer.Email);
            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory]
        [InlineData("", "حسنی", "javidleo.ef@gmail.com")]
        [InlineData("javid", "", "javidleo.ef@gmail.com")]
        [InlineData("javid", "حسنی", "")]
        [InlineData("jav#", "حسنی", "javidleo.ef@gmail.com")]
        [InlineData("javid", "حسنی$", "javidleo.ef@gmail.com")]
        [InlineData("javid", "حسنی", "javidleo.ef.com")]
        public void CreateCustomer_CheckForInvalidData_ThrowsNotAcceptableException(string name, string family, string email)
        {
            void result() => _customerService.Create(name, family, email);
            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void CreateCustomer_CheckForDuplicateEmailAddress_ThrowsDuplicateException()
        {
            _customerFakeRepository.SetExistingEmail(_customer.Email);

            void result() => _customerService.Create(_customer.Name, _customer.Family, _customer.Email);
            Assert.Throws<ConflictException>(result);
        }
    }
}
