using DataAccess.Mapping;
using DomainModel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class CinemaContext : DbContext
    {
        public CinemaContext() { }
        public CinemaContext(DbContextOptions<CinemaContext> option) : base(option) { }

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

        //public IConfigurationRoot Configuration { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Cinema;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply Mappings 
            modelBuilder.ApplyConfiguration(new TicketMapping());
            modelBuilder.ApplyConfiguration(new ChairMapping());
            modelBuilder.ApplyConfiguration(new ChairActivityMapping());
            modelBuilder.ApplyConfiguration(new SalonMapping());
            modelBuilder.ApplyConfiguration(new SalonActivityMapping());
            modelBuilder.ApplyConfiguration(new CinemaMapping());
            modelBuilder.ApplyConfiguration(new CinemaActivityMapping());
            modelBuilder.ApplyConfiguration(new CustomerMapping());
            modelBuilder.ApplyConfiguration(new CityMapping());
            modelBuilder.ApplyConfiguration(new ProvinceMapping());
            modelBuilder.ApplyConfiguration(new MovieMapping());
            modelBuilder.ApplyConfiguration(new MovieActoresMapping());
            modelBuilder.ApplyConfiguration(new SansMapping());
            modelBuilder.ApplyConfiguration(new MovieSansSalonMapping());
        }
    }
}