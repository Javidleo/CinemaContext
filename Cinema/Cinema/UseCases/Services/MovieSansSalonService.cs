using DomainModel;
using System;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;

namespace UseCases.Services
{
    public class MovieSansSalonService : IMovieSansSalonService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly ISansRepository _sansRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMovieSansSalonRepository _movieSansSalonRepository;

        public MovieSansSalonService(IMovieRepository movieRepository, ISalonRepository salonRepository, ISansRepository sansRepository
                                    , IAdminRepository adminRepository, IMovieSansSalonRepository movieSansSalonRepository)
        {
            _movieRepository = movieRepository;
            _salonRepository = salonRepository;
            _sansRepository = sansRepository;
            _adminRepository = adminRepository;
            _movieSansSalonRepository = movieSansSalonRepository;
        }

        public Task Create(int movieId, int salonId, int sansId, Guid adminGuid)
        {
            if (!_movieRepository.DoesExist(movieId))
                throw new NotFoundException("movie not found");

            if (!_salonRepository.DoesExist(salonId))
                throw new NotFoundException("salon not found");

            if (!_sansRepository.DoesExist(sansId))
                throw new NotFoundException("sans not found");

            if (adminGuid == Guid.Empty)
                throw new NotAcceptableException("invalid adminId");

            MovieSansSalon movieSansSalon = MovieSansSalon.Create(movieId, salonId, sansId, adminGuid);

            _movieSansSalonRepository.Add(movieSansSalon);
            return Task.CompletedTask;
        }
    }
}