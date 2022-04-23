using DomainModel;
using System;
using System.Collections.Generic;

namespace UseCases.RepositoryContract
{
    public interface IMovieSansSalonRepository
    {
        void Add(MovieSansSalon obj);
        List<MovieSansSalon> FindMovie(int movieId, int cityId, DateTime premiereDate);
        MovieSansSalon FindMovie(int movieId, int cityId);
    }
}