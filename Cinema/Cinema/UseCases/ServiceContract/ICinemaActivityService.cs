using DomainModel.Domain;
using DomainModel.Validation;
using NEGSO.Utilities;
using System;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;

namespace UseCases.ServiceContract
{
    public class CinemaActivityService : ICinemaActivityService
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly ICinemaActivityRepository _cinemaActivityRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly CinemaActivityValidator _validator;
        public CinemaActivityService(ICinemaRepository cinemaRepository, ICinemaActivityRepository cinemaActivityRepository,IAdminRepository adminRepository)
        {
            _cinemaRepository = cinemaRepository;
            _cinemaActivityRepository = cinemaActivityRepository;
            _adminRepository = adminRepository;
            _validator = new CinemaActivityValidator();
        }

        public Task Deactivate(int cinemaId, string description,DateTime startDate, Guid adminGuid, string adminFullName)
        {
            if (!_cinemaRepository.DoesExist(cinemaId))
                throw new NotFoundException("cinema not found");

            if (!_adminRepository.DoesExist(adminGuid))
                throw new NotFoundException("admin not found");

            CinemaActivity activity = CinemaActivity
                .Create(cinemaId, startDate,startDate.ToPersianDate(), description, adminGuid, adminFullName);

            if (!_validator.Validate(activity).IsValid)
                throw new NotAcceptableException("invalid cinemaActivity");

            _cinemaActivityRepository.Add(activity);
            return Task.CompletedTask;
        }
    }
}