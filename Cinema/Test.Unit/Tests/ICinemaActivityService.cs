using DomainModel.Domain;
using DomainModel.Validation;
using NEGSO.Utilities;
using System;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;

namespace Test.Unit.Tests
{
    public class CinemaActivityService : ICinemaActivityService
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly AdminValidator _adminValidator;
        private readonly ICinemaActivityRepository _cinemaActivityRepository;
        public CinemaActivityService(ICinemaRepository cinemaRepository, ICinemaActivityRepository cinemaActivityRepository)
        {
            _cinemaRepository = cinemaRepository;
            _adminValidator = new AdminValidator();
            _cinemaActivityRepository = cinemaActivityRepository;
        }

        public Task Deactivate(int cinemaId, string description,Guid adminGuid,string adminFullName)
        {
            if (!_cinemaRepository.DoesExist(cinemaId))
                throw new NotFoundException("cinema not found");

            if (adminGuid == Guid.Empty)
                throw new NotAcceptableException("invalid adminId");

            if (string.IsNullOrWhiteSpace(adminFullName))
                throw new NotAcceptableException("admin name is empty");

            CinemaActivity activity = CinemaActivity.Create
                    (cinemaId, DateTime.Now.Date, DateTime.Now.ToPersianDate(), description, adminGuid, adminFullName);

            _cinemaActivityRepository.Add(activity);
            return Task.CompletedTask;
        }
    }
}