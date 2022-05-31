using DataAccess.Context;
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
    public class AdminTests
    {
        private readonly IAdminService _adminService;
        private readonly CinemaFakeRepository _cinemaFakeRepository;
        private readonly AdminFakeRepository _adminFakeRepository;
        private readonly AdminValidator _validator;

        public AdminTests()
        {
            var cinemaContext = new CinemaContext();
            _adminFakeRepository = new AdminFakeRepository(cinemaContext);
            _cinemaFakeRepository = new CinemaFakeRepository(cinemaContext);
            _adminService = new AdminService(_adminFakeRepository, _cinemaFakeRepository);
            _validator = new AdminValidator();
        }

        [Theory]
        [InlineData("", "name is empty")]
        [InlineData("Fds$%@", "invalid name")]
        [InlineData("%234sdf", "invalid name")]
        public void AdminValidation_CheckForNameRuleSetValidation_ThrowValidationException(string name, string exceptionMessage)
        {
            var admin = new AdminBuilder().WithName(name).Build();

            var result = _validator.TestValidate(admin, option => option.IncludeRuleSets("Name"));
            result.ShouldHaveValidationErrorFor(i => i.Name).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData("", "family is empty")]
        [InlineData("famiy%", "invalid family")]
        public void AdminValidation_CheckForFamilyValidation_ThrowValidationException(string family, string exceptionMessage)
        {
            var admin = new AdminBuilder().WithFamily(family).Build();

            var result = _validator.TestValidate(admin, option => option.IncludeRuleSets("Name"));
            result.ShouldHaveValidationErrorFor(i => i.Family).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData("34df234o4i12u4o5s", "invalid nationalCode")]
        [InlineData("42314235412", "invalid nationalCode")]
        [InlineData("12hkjbxdkj", "invalid nationalCode")]
        [InlineData("123123123123", "invalid nationalCode")]
        public void AdminValidation_CheckForNationalCodeValidation_ThrowValidationException(string nationalCode, string exceptionMessage)
        {
            var admin = new AdminBuilder().WithNationalCode(nationalCode).Build();

            var result = _validator.TestValidate(admin, option => option.IncludeRuleSets("NationalCode"));
            result.ShouldHaveValidationErrorFor(i => i.NationalCode).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData("", "email is empty")]
        [InlineData("email", "invalid email")]
        [InlineData("email#@", "invalid email")]
        [InlineData("email%", "invalid email")]
        [InlineData("emila.com", "invalid email")]
        public void AdminValidation_CheckForEmailValidation_ThrowValidationException(string email, string exceptionMessage)
        {
            var admin = new AdminBuilder().WithEmail(email).Build();

            var result = _validator.TestValidate(admin, option => option.IncludeRuleSets("UserName&Email"));
            result.ShouldHaveValidationErrorFor(i => i.Email).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData("user%", "invalid userName")]
        [InlineData("", "userName is empty")]
        [InlineData("User234", "invalid userName")]
        [InlineData("User", "invalid userName")]
        public void AdminValidation_CheckForUserNameValidation_ThrowValidationException(string userName, string exceptionMessage)
        {
            var admin = new AdminBuilder().WithUserName(userName).Build();

            var result = _validator.TestValidate(admin, option => option.IncludeRuleSets("UserName&Email"));
            result.ShouldHaveValidationErrorFor(i => i.UserName).WithErrorMessage(exceptionMessage);
        }

        [Theory]
        [InlineData("", "invalid password")]
        [InlineData("fdsfsd", "invalid password")]
        [InlineData("fsdf324", "invalid password")]
        [InlineData("1231233", "invalid password")]
        [InlineData("fffffff", "invalid password")]
        public void AdminValidation_CheckForPassword_ThrowValidationException(string password, string exceptionMessage)
        {
            var admin = new AdminBuilder().WithPassword(password).Build();

            var result = _validator.TestValidate(admin, option => option.IncludeRuleSets("Password"));
            result.ShouldHaveValidationErrorFor(i => i.Password).WithErrorMessage(exceptionMessage);
        }
        [Fact]
        public void CreateAdmin_CheckForValidation_ThrowNotAcceptableException()
        {
            var admin = new AdminBuilder().WithName("").Build();
            _cinemaFakeRepository.SetExistingId(admin.CinemaId);

            void result() => _adminService.Create(admin.CinemaId, admin.Name, admin.Family, admin.NationalCode, admin.Email, admin.UserName, admin.Password);

            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void CreateAdmin_CheckForInvalidCinemaId_ThrowsNotFoundException()
        {
            var admin = new AdminBuilder().Build();

            void result() => _adminService.Create(admin.CinemaId, admin.Name, admin.Family, admin.NationalCode, admin.Email, admin.UserName, admin.Password);
            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void CreateAdmin_CheckForWorkingWell()
        {
            var admin = new AdminBuilder().Build();
            _cinemaFakeRepository.SetExistingId(admin.CinemaId);

            var result = _adminService.Create(admin.CinemaId, admin.Name, admin.Family, admin.NationalCode, admin.Email, admin.UserName, admin.Password);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        // in this case an admin should send email or username then we need to check some admin with this information exist in database or nots 
        [Fact]
        public void LoginAdmin_CheckForWorkingWellWithEmail()
        {
            var admin = new AdminBuilder().Build();
            _adminFakeRepository.SetExistingEmail(admin.Email);

            var result = _adminService.Find(admin.Email, admin.Password);
            result.Result.Should().BeEquivalentTo(admin);
        }

        [Fact]
        public void LoginAdmin_CheckForInvalidEmail_ThorwNotAcceptableException()
        {
            var admin = new AdminBuilder().WithEmail("javid@flwkenr!.c").Build();

            void result() => _adminService.Find(admin.Email, admin.Password);
            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void LoginAdmin_CheckForNotExistedEmail_ThrowNotAcceptableException()
        {
            var admin = new AdminBuilder().Build();

            void result() => _adminService.Find(admin.Email, admin.Password);
            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void LoginAdmin_CheckForWorkingWellWithUserName()
        {
            var admin = new AdminBuilder().Build();
            _adminFakeRepository.SetExistingUserName(admin.UserName);

            var result = _adminService.Find(admin.UserName, admin.Password);
            result.Result.Should().BeEquivalentTo(admin);
        }

        [Theory]
        [InlineData("Javid")]
        [InlineData("javid$123")]
        [InlineData("ja$%@")]
        [InlineData("!jav36-2,")]
        public void LoginAdmin_CheckForInvalidUserName_ThrowNotAcceptableException(string userName)
        {
            var admin = new AdminBuilder().WithUserName(userName).Build();

            void result() => _adminService.Find(admin.UserName, admin.Password);
            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void LoginAdmin_CheckForNonExisted_ThorwsNotFoundException()
        {
            var admin = new AdminBuilder().Build();

            void result() => _adminService.Find(admin.UserName, admin.Password);
            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void UpdateAdmin_CheckForWorkingWell()
        {
            var admin = new AdminBuilder().Build();
            _adminFakeRepository.SetExistingGuid(admin.AdminGuid);

            var result = _adminService.Modify(admin.AdminGuid, admin.Name, admin.Family, admin.Email, admin.UserName);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void UpdateAdmin_CheckForInvalidAdminId_ThrowNotFoundException()
        {
            var admin = new AdminBuilder().Build();

            void result() => _adminService.Modify(admin.AdminGuid, admin.Name, admin.Family, admin.Email, admin.UserName);

            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void UpdateAdmin_CheckForValidation_ThrowNotAcceptableExcetpion()
        {
            var admin = new AdminBuilder().WithName("").Build();
            _adminFakeRepository.SetExistingGuid(admin.AdminGuid);

            void result() => _adminService.Modify(admin.AdminGuid, admin.Name, admin.Family, admin.Email, admin.UserName);

            Assert.Throws<NotAcceptableException>(result);
        }

        [Fact]
        public void ChangeAdminPassword_CheckForWorkingWell()
        {
            var admin = new AdminBuilder().Build();
            _adminFakeRepository.SetExistingGuid(admin.AdminGuid);

            var result = _adminService.ChangePasword(admin.AdminGuid, admin.Password);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Fact]
        public void ChangeAdminPassword_CheckForInvalidAdminid_ThrowNotFoundException()
        {
            var admin = new AdminBuilder().Build();

            void result() => _adminService.ChangePasword(admin.AdminGuid, admin.Password);

            Assert.Throws<NotFoundException>(result);
        }

        [Fact]
        public void ChangeAdminPassword_CheckForValidation_ThrowNotAcceptableExcetpion()
        {
            var admin = new AdminBuilder().WithPassword("admin").Build();
            _adminFakeRepository.SetExistingGuid(admin.AdminGuid);

            void result() => _adminService.ChangePasword(admin.AdminGuid, admin.Password);

            Assert.Throws<NotAcceptableException>(result);
        }
    }
}
