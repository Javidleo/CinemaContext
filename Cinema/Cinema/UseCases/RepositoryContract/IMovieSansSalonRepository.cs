using DomainModel.Domain;
using System;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IMovieSansSalonRepository
    {
        void Add(MovieSansSalon obj);
        List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId, DateTime premiereDate);
        List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId);
    }
}