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
        private readonly AdminValidator _adminValidator;
        private readonly ICinemaActivityRepository _cinemaActivityRepository;
        private readonly CinemaActivityValidator _validator;
        public CinemaActivityService(ICinemaRepository cinemaRepository, ICinemaActivityRepository cinemaActivityRepository)
        {
            _cinemaRepository = cinemaRepository;
            _adminValidator = new AdminValidator();
            _cinemaActivityRepository = cinemaActivityRepository;
            _validator = new CinemaActivityValidator();
        }

        public Task Deactivate(int cinemaId, string description, Guid adminGuid, string adminFullName)
        {
            if (!_cinemaRepository.DoesExist(cinemaId))
                throw new NotFoundException("cinema not found");

            CinemaActivity activity = CinemaActivity.Create
                    (cinemaId, DateTime.Now.Date, DateTime.Now.ToPersianDate(), description, adminGuid, adminFullName);

            if (!_validator.Validate(activity).IsValid)
                throw new NotAcceptableException("invalid cinemaActivity");

            _cinemaActivityRepository.Add(activity);
            return Task.CompletedTask;
        }
    }
}