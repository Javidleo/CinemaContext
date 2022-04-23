using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UseCases.RepositoryContract;

namespace DataAccess.Repository
{
    public class MovieSansSalonRepository : IMovieSansSalonRepository
    {
        private readonly CinemaContext _context;
        public MovieSansSalonRepository()
        => _context = new CinemaContext();

        public void Add(MovieSansSalon obj)
        {
            _context.MovieSansSalon.Add(obj);
            _context.SaveChanges();
        }

        public List<MovieSansSalon> FindMovie(int movieId, int cityId, DateTime premiereDate)
        => _context.MovieSansSalon
                .Include(i => i.Salon).ThenInclude(i => i.Cinema).ThenInclude(i => i.City)
                        .Where(i => i.MovieId == movieId && i.Salon.Cinema.City.Id == cityId && i.PremiereDate == premiereDate.Date).ToList();

        public MovieSansSalon FindMovie(int movieId,int cityId)
        => _context.MovieSansSalon
                .Include(i => i.Salon).ThenInclude(i => i.Cinema).ThenInclude(i => i.City)
                            .FirstOrDefault(i => i.MovieId == movieId && i.Salon.Cinema.City.Id == cityId);

    }
}
