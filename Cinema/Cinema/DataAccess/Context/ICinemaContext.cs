using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Context
{
    public interface ICinemaContext
    {
        DbSet<TEntity> Set<TEntity>(Type type) where TEntity : class;
        int SaveChanges();

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Chair> Chair { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Salon> Salon { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<SalonActivity> SalonActivity { get; set; }
        public DbSet<ChairActivity> ChairActivity { get; set; }
        public DbSet<CinemaActivity> CinemaActivity { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieSansSalon> MovieSansSalon { get; set; }
        public DbSet<Sans> Sans { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Province> Province { get; set; }
    }
}
