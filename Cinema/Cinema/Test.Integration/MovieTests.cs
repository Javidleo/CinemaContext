using DataAccess.Context;
using DomainModel.Domain;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Test.Integration
{
    public class MovieTests :PersistTest<CinemaContext>
    {
        private readonly CinemaContext _context;
        private readonly ContextOptionBuilderGenerator<CinemaContext> _generator;
        public MovieTests()
        {
            _generator = new ContextOptionBuilderGenerator<CinemaContext>();
            _context = new CinemaContext(_generator.Build().Options);
        }

        [Fact]
        public void CreateMovie_CheckForWorkingWell()
        {
            var actor = MovieActores.Create("javid", "fatemeh", "ali", "nahid",Guid.NewGuid(),"alireza hasani");
            var movie = Movie.Create(Guid.NewGuid(), "amin", "name", "director", "producer", DateTime.Now.Date, "11/12/1212", actor);

            _context.Movie.Add(movie);
            _context.SaveChanges();

            var result = _context.Movie.FirstOrDefault(i => i.Name == movie.Name);
            result.Should().BeEquivalentTo(movie);
        }

        [Fact]
        public void CreateProvince()
        {
            var province = Province.Create("ahmad");
            _context.Province.Add(province);
            _context.SaveChanges();

            var result = _context.Province.FirstOrDefault(i=> i.Name == province.Name);
            result.Should().BeEquivalentTo(province);
        }
        [Fact]
        public void CreateCity()
        {
            var city = City.Create("ghir", 6);
            _context.City.Add(city);
            _context.SaveChanges();

            var result = _context.City.FirstOrDefault(i => i.Name == city.Name);
            result.Should().Be(city);
        }

        [Fact]
        public void CreateCinema()
        {
            var province = Province.Create("fars");

            var cinema = Cinema.Create("aftab", 20000, "new state west", 3);
            _context.Cinema.Add(cinema);
            _context.SaveChanges();

            var result = _context.Cinema.FirstOrDefault(i => i.Name == cinema.Name);
            result.Should().BeEquivalentTo(cinema);
        }

        [Fact]
        public void CreateCinemaActivity()
        {
            var cinemaActivity = CinemaActivity.Create(1, DateTime.Now.Date, "11/12/1399", "", Guid.NewGuid(), "javid rezaie");
            _context.CinemaActivity.Add(cinemaActivity);
            _context.SaveChanges();

            var result = _context.CinemaActivity.FirstOrDefault(i => i.StartDate == DateTime.Now.Date);
            result.Should().BeEquivalentTo(cinemaActivity);
        }

        [Fact]
        public void CreateSalon()
        {
            var salon = Salon.Create(1, "salonb", 200);
            _context.Salon.Add(salon);
            _context.SaveChanges();

            var result = _context.Salon.First(i => i.Name == salon.Name);
            result.Should().BeEquivalentTo(salon);
        }
        [Fact]
        public void CreatesalonActivity()
        {
            var a = SalonActivity.Create(2, DateTime.Now.Date, "11/12/1399", "string", Guid.NewGuid(), "adminfullname");
            _context.SalonActivity.Add(a);
            _context.SaveChanges();

            var result = _context.SalonActivity.First(i => i.StartDate == DateTime.Now.Date);
            result.Should().BeEquivalentTo(a);
        }

        [Fact]
        public void CreateChair()
        {
            var chair = Chair.Create(2,10,10);
            _context.Chair.Add(chair);
            _context.SaveChanges();

            var result = _context.Chair.First(i=> i.Number == chair.Number && i.Row == chair.Row);
            result.Should().BeEquivalentTo(chair);
        }
        
        [Fact]
        public void CreateChairActivty()
        {
            var chairactivity = ChairActivity.Create(7, DateTime.Now.Date, "11/12/1399", "descritpion", Guid.NewGuid(), "adminfullname");
            _context.ChairActivity.Add(chairactivity);
            _context.SaveChanges();

            var result = _context.ChairActivity.First(i => i.StartDate == DateTime.Now.Date);
            result.Should().BeEquivalentTo(chairactivity);
        }

        [Fact]
        public void Createprovince()
        {
            var province = Province.Create("name");
            _context.Province.Add(province);
            _context.SaveChanges();

            var result = _context.Province.First(i => i.Name == province.Name);
            result.Should().BeEquivalentTo(province);
        }

        // test it later
        [Fact]
        public void CreateTicket()
        {
            var ticket = Ticket.Create(null,2,2,1,2,20000);
            _context.Ticket.Add(ticket);
            _context.SaveChanges();

            var result = _context.Ticket.First(i=> i.Price == ticket.Price);
            result.Should().BeEquivalentTo(ticket);
        }

        //[Fact]
        //public void CreateCustomer()
        //{
        //    var customer = Customer.Create("name", "family", "email");
        //    _context.Customer.Add(customer);
        //    _context.SaveChanges();

        //    var result = _context.Customer.First(i => i.Name == customer.Name);
        //    result.Should().BeEquivalentTo(customer);
        //}

        [Fact]
        public void moviewithmovieactores()
        {
            var movieactor = MovieActores.Create("ali", "nahid", "reza", "shabnam", Guid.NewGuid(), "admin");
            var movie = Movie.Create(Guid.NewGuid(), "admin", "name", "director,", "producer", DateTime.Now.Date, "persian", movieactor);

            _context.Movie.Add(movie);
            _context.SaveChanges();
            var result = _context.Movie.First(i=> i.Name == movie.Name);
            result.Should().BeEquivalentTo(movie);
        }

        [Fact]
        public void CreateSans()
        {
            var sans = Sans.Create("11:00-12:30");
            _context.Sans.Add(sans);
            _context.SaveChanges();

            var result = _context.Sans.First(i => i.Name == sans.Name);
            result.Should().BeEquivalentTo(sans);
        }

        [Fact]
        public void CreateMovieSansSalon()
        {
            var obj = MovieSansSalon.Create(2, 2, 1, Guid.NewGuid(), "admin",DateTime.Now.Date,"11/12/1399");
            _context.MovieSansSalon.Add(obj);
            _context.SaveChanges();

            var result = _context.MovieSansSalon.First(i => i.AdminFullName == "admin");
            result.Should().BeEquivalentTo(obj);
        }
    }
}
