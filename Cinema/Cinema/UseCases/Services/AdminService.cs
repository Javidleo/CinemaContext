using DomainModel.Domain;
using DomainModel.Validation;
using FluentValidation;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class AdminService : IAdminService
    {
        private readonly AdminValidator _validator;
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly BaseValidation _baseValidation;
        public AdminService(IAdminRepository adminRepository, ICinemaRepository cinemaRepository)
        {
            _adminRepository = adminRepository;
            _cinemaRepository = cinemaRepository;
            _validator = new AdminValidator();
            _baseValidation = new BaseValidation();
        }

        public Task Create(int cinemaId, string name, string family, string nationalCode, string email,
                        string userName, string password)
        {
            if (!_cinemaRepository.DoesExist(cinemaId))
                throw new NotFoundException("cinema not found");

            Admin admin = Admin.Create(cinemaId, name, family, nationalCode, email, userName, password);

            if (!_validator.Validate(admin, option => option.IncludeRuleSets("*")).IsValid)// * use all rulesets for validation
                throw new NotAcceptableException("invalid input values");

            return Task.CompletedTask;
        }

        // key can be uesrname or email
        public Task<Admin> Find(string key, string password)
        {
            if (key.Contains('@'))
            {
                if (!Regex.IsMatch(key, _baseValidation.EmailRegex))
                    throw new NotAcceptableException("invalid email");

                var admin = _adminRepository.FindByEmail(key);
                if (admin is null)
                    throw new NotFoundException("admin not found");

                return Task.FromResult(admin);
            }
            else
            {
                if (!Regex.IsMatch(key, _baseValidation.LowerCaseEnglish_NumbersRegex))
                    throw new NotAcceptableException("invalid userName");

                var admin = _adminRepository.FindByUserName(key);
                if (admin is null)
                    throw new NotFoundException("adin not found");

                return Task.FromResult(admin);
            }
        }

        public Task Modify(Guid adminGuid, string name, string family, string email, string userName)
        {
            var admin = _adminRepository.Find(adminGuid);
            if (admin is null)
                throw new NotFoundException("admin not found");

            admin.Modify(name, family, email, userName);

            if (!_validator.Validate(admin, options => options.IncludeRuleSets("Name", "UserName&Email")).IsValid)
                throw new NotAcceptableException("invalid admin");

            return Task.CompletedTask;
        }

        public Task ChangePasword(Guid adminGuid, string password)
        {
            var admin = _adminRepository.Find(adminGuid);
            if (admin is null)
                throw new NotFoundException("admin not found");

            admin.ChangePassword(password);

            if (!_validator.Validate(admin, option => option.IncludeRuleSets("Password")).IsValid)
                throw new NotAcceptableException("week password");

            return Task.CompletedTask;
        }
    }
}