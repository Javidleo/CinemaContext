using DomainModel.Domain;
using DomainModel.Validation;
using NEGSO.Utilities;
using System;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class SalonActivityService : ISalonActivityService
    {
        private readonly ISalonActivityRepository _salonActivityRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly SalonActivityValidator _validator;

        public SalonActivityService(ISalonActivityRepository salonActivityRepository, ISalonRepository salonRepository, IAdminRepository adminRepository)
        {
            _salonActivityRepository = salonActivityRepository;
            _salonRepository = salonRepository;
            _adminRepository = adminRepository;
            _validator = new SalonActivityValidator();
        }

        public Task Deactivate(int salonId, string description, DateTime startDate, Guid adminGuid, string adminFullName)
        {
            if (!_salonRepository.DoesExist(salonId))
                throw new NotFoundException("salon not found");

            if (!_adminRepository.DoesExist(adminGuid))
                throw new NotFoundException("admin not found");

            SalonActivity salonActivity = SalonActivity
                .Create(salonId, startDate, startDate.ToPersianDate(), description, adminGuid, adminFullName);

            if (!_validator.Validate(salonActivity).IsValid)
                throw new NotAcceptableException("invalid salonacitivty");

            _salonActivityRepository.Add(salonActivity);
            return Task.CompletedTask;
        }
    }
}