using DomainModel.Domain;
using System;
using System.Collections.Generic;
using UseCases.RepositoryContract.Abstraction;

namespace UseCases.RepositoryContract
{
    public interface IMovieSansSalonRepository : IBaseRepository<MovieSansSalon>
    {
        List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId, DateTime premiereDate);
        List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId);
        bool DoesExist(int movieSansSalonId);
    }
}