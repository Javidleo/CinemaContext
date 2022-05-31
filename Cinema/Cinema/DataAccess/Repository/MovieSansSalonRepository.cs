using DataAccess.Context;
using DataAccess.Repository.Abstraction;
using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class MovieSansSalonRepository :BaseRepository<MovieSansSalon>, IMovieSansSalonRepository
    {
        private readonly ICinemaContext _context;
        public MovieSansSalonRepository(ICinemaContext context) : base(context)
        => _context = context;

        public List<MovieSansSalon> FindOnScreenMovies(int movieId, int cityId, DateTime premiereDate)
        => _context.MovieSansSalon
                .Include(i => i.Salon).ThenInclude(i => i.Cinema).ThenInclude(i => i.City)
                        .Where(i => i.MovieId == movieId && i.Salon.Cinema.City.Id == cityId && i.PremiereDate == premiereDate.Date).ToList();

        public List<MovieSansSalon> FindOnScreenMovies(int movieId,int cityId)
        => _context.MovieSansSalon
                .Include(i => i.Salon).ThenInclude(i => i.Cinema).ThenInclude(i => i.City)
                            .Where(i => i.MovieId == movieId && i.Salon.Cinema.City.Id == cityId).ToList();
        public bool DoesExist(int Id)
        => _context.MovieSansSalon.Any(i => i.Id == Id);
    }
}
