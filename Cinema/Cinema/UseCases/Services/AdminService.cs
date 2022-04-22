using DomainModel;
using DomainModel.Validation;
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
        public AdminService(IAdminRepository adminRepository, ICinemaRepository cinemaRepository)
        {
            _adminRepository = adminRepository;
            _cinemaRepository = cinemaRepository;
            _validator = new AdminValidator();
        }

        public Task Create(int cinemaId, string name, string family, string nationalCode, string email,string userName, string password)
        {
            if (!_cinemaRepository.DoesExist(cinemaId))
                throw new NotFoundException("cinema not found");

            Admin admin = Admin.Create(cinemaId,name,family,nationalCode,email,userName,password);

            if (!_validator.Validate(admin).IsValid)
                throw new NotAcceptableException("invalid input values");

            return Task.CompletedTask;
        }

        // key can be uesrname or email
        public Task<Admin> Find(string key,string password)
        {
            if (key.Contains('@'))
            {
                if (! Regex.IsMatch(key, "^[a-z0-9+_.-]+@[a-zA-Z0-9.-]+$"))
                    throw new NotAcceptableException("invalid email");

                var admin = _adminRepository.FindByEmail(key);
                if (admin is null)
                    throw new NotFoundException("admin not found");

                return Task.FromResult(admin);
            }
            else
            {
                if (!Regex.IsMatch(key, "^[a-z0-9]+$"))
                    throw new NotAcceptableException("invalid userName");

                var admin = _adminRepository.FindByUserName(key);
                if (admin is null)
                    throw new NotFoundException("adin not found");

                return Task.FromResult(admin);
            }
        }
    }
}