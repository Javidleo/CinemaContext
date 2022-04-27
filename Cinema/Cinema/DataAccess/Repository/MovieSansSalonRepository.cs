using DomainModel.Domain;
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

        public List<MovieSansSalon> GetAll()
        => _context.MovieSansSalon.Include(i => i.Salon).ThenInclude(i => i.Cinema).ThenInclude(i => i.City).Include(i=> i.Sans).ToList();
    }
}
