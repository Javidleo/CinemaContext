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
    public class MovieService : IMovieService
    {
        private readonly MovieValidator _movieValidator;
        private readonly MovieActoresValidator _actoresValidator;
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            _movieValidator = new MovieValidator();
            _actoresValidator = new MovieActoresValidator();
        }

        public Task Create(Guid adminGuid, string adminFullName, string name, string director, string producer
            , DateTime publishDate, string baseMaleActorName, string baseFemaleActorName, string supportedMaleActorName, string supportedFemaleActorName)
        {
            MovieActores actores = MovieActores.Create(baseMaleActorName, baseFemaleActorName, supportedMaleActorName, supportedFemaleActorName);

            Movie movie = Movie.Create(adminGuid, adminFullName, name, director, producer, publishDate, publishDate.ToPersianDate(), actores);

            if (!_actoresValidator.Validate(actores).IsValid)
                throw new NotAcceptableException("invalid movie actors");

            if (!_movieValidator.Validate(movie).IsValid)
                throw new NotAcceptableException("invalid movie");

            _movieRepository.Add(movie);

            return Task.CompletedTask;
        }


    }
}