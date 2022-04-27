using DomainModel.Domain;
using DomainModel.Validation;
using NEGSO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCases.Exceptions;
using UseCases.RepositoryContract;
using UseCases.ServiceContract;
using UseCases.ViewModels;

namespace UseCases.Services
{
    public class MovieSansSalonService : IMovieSansSalonService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly ISansRepository _sansRepository;
        private readonly IMovieSansSalonRepository _movieSansSalonRepository;
        private readonly MovieSansSalonValidator _validator;

        public MovieSansSalonService(IMovieRepository movieRepository, ISalonRepository salonRepository, ISansRepository sansRepository
                                 , IMovieSansSalonRepository movieSansSalonRepository)
        {
            _movieRepository = movieRepository;
            _salonRepository = salonRepository;
            _sansRepository = sansRepository;
            _movieSansSalonRepository = movieSansSalonRepository;
            _validator = new MovieSansSalonValidator();
        }

        public Task Create(int movieId, int salonId, int sansId, Guid adminGuid,string adminFullName,DateTime premiereDate)
        {
            if (! _movieRepository.DoesExist(movieId))
                throw new NotFoundException("movie not found");

            if (! _sansRepository.DoesExist(sansId))
                throw new NotFoundException("sans not found");

            var salon = _salonRepository.FindWithParents(salonId);

            if (salon is null)
                throw new NotFoundException("slon not found");

            if (salon.Cinema.CinemaActivities.Any(i => i.EndDate == null))
                throw new NotAcceptableException("cinema is deactivated please check the cinema status");

            string premiereDatePersian = premiereDate.ToPersianDate();

            MovieSansSalon movieSansSalon = MovieSansSalon.Create(movieId, salonId, sansId, adminGuid, adminFullName, premiereDate,                                                                     premiereDatePersian);

            if (!_validator.Validate(movieSansSalon).IsValid)
                throw new NotAcceptableException("invalid movieSansSalon");

            _movieSansSalonRepository.Add(movieSansSalon);
            return Task.CompletedTask;
        }

        public Task<List<GetMovieByCityViewModel>> GetMovieByCity(int movieId, int cityId, DateTime premiereDate)
        {
            var movies = _movieSansSalonRepository.FindOnScreenMovies(movieId, cityId, premiereDate);

            var result = new List<GetMovieByCityViewModel>();
            var viewModelItem = new GetMovieByCityViewModel();

            foreach (var movie in movies)
            {
                viewModelItem.CinemaName = movie.Salon.Cinema.Name;
                viewModelItem.SansName = movie.Sans.Name;
                viewModelItem.CinemaAddress = movie.Salon.Cinema.Address;
                viewModelItem.TicketPrice = movie.Salon.Cinema.TicketPrice;

                result.Add(viewModelItem);
            }
            return Task.FromResult(result);
        }

        public Task<List<GetMovieByCityViewModel>> GetMovieByCity(int movieId, int cityId)
        {
            var movies = _movieSansSalonRepository.FindOnScreenMovies(movieId, cityId);

            var result = new List<GetMovieByCityViewModel>();
            
            foreach (var movie in movies)
            {
                var viewModelItem = new GetMovieByCityViewModel();

                viewModelItem.CinemaName = movie.Salon.Cinema.Name;
                viewModelItem.CinemaAddress = movie.Salon.Cinema.Address;
                viewModelItem.SansName = movie.Sans.Name;
                viewModelItem.TicketPrice = movie.Salon.Cinema.TicketPrice;

                result.Add(viewModelItem);
            }
            return Task.FromResult(result);
        }

        public Task<List<GetMovieByCityViewModel>> GetAll()
        {
            var movies = _movieSansSalonRepository.GetAll();
            var result = new List<GetMovieByCityViewModel>();

            foreach(var movie in movies)
            {
                var viewModelItem = new GetMovieByCityViewModel();

                viewModelItem.CinemaName = movie.Salon.Cinema.Name;
                viewModelItem.CinemaAddress = movie.Salon.Cinema.Address;
                viewModelItem.SansName = movie.Sans.Name;
                viewModelItem.TicketPrice = movie.Salon.Cinema.TicketPrice;
                result.Add(viewModelItem);
            }
            return Task.FromResult(result);
        }
    }
}