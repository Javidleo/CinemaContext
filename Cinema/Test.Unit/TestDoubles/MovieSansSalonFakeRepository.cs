using DomainModel.Domain;
using System;
using System.Collections.Generic;
using Test.Unit.builders;
using UseCases.RepositoryContract;

namespace Test.Unit.TestDoubles
{
    public class MovieSansSalonFakeRepository : IMovieSansSalonRepository
    {
        private int _existingId;
        public void SetExistingId(int id) => _existingId = id;

        public void Add(MovieSansSalon obj)
        {

        }

        public bool DoesExist(int movieSansSalonId)
        {
            if (movieSansSalonId == _existingId) return true;
            return false;
        }

        public List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId, DateTime premiereDate)
        => throw new NotImplementedException(); // use mock insted

        public List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId)
        => throw new NotImplementedException(); // use mock insted

        public List<MovieSansSalon> GetAll()
        {
            return new List<MovieSansSalon>() { new MovieSansSalonBuilder().Build() };
        }
    }
}