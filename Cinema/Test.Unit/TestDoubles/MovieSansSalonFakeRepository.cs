using DomainModel.Domain;
using System;
using System.Collections.Generic;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    public class MovieSansSalonFakeRepository : IMovieSansSalonRepository
    {
        public void Add(MovieSansSalon obj)
        {

        }

        public List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId, DateTime premiereDate)
        {
            throw new NotImplementedException();
        }

        public List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId)
        {
            throw new NotImplementedException();
        }
    }
}