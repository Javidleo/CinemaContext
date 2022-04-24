using DomainModel.Domain;
using DomainModel.Validation;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class SalonService : ISalonService
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly SalonValidator _salonValidator;
        public SalonService(ICinemaRepository cinemaRepository, ISalonRepository salonRepository)
        {
            _cinemaRepository = cinemaRepository;
            _salonRepository = salonRepository;
            _salonValidator = new SalonValidator();
        }

        public Task Create(int cinemaId, string name, int capacity)
        {
            if (!_cinemaRepository.DoesExist(cinemaId))
                throw new NotFoundException("cinema not found");

            Salon salon = Salon.Create(cinemaId, name, capacity);

            if (!_salonValidator.Validate(salon).IsValid)
                throw new NotAcceptableException("invalid salon");

            _salonRepository.Add(salon);
            return Task.CompletedTask;
        }
    }
}