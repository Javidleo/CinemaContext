using DataAccess;
using DomainModel;
using System;
using Xunit;
using System.Linq;
using FluentAssertions;

namespace Test.Integration
{
    public class MovieTests: PersistTest<CinemaContext>
    {
        private readonly CinemaContext _context;
        private readonly ContextOptionBuilderGenerator _generator;
        public MovieTests()
        {
            _generator = new ContextOptionBuilderGenerator();
            _context = new CinemaContext(_generator.Build().Options);
        }

        //[Fact]
        //public void CreateMovie_CheckForWorkingWell()
        //{
        //    var actor = MovieActores.Create("javid", "fatemeh", "ali", "nahid");
        //    var movie = Movie.Create(Guid.NewGuid(), "name", "director", "producer", DateTime.Now, DateTime.Now.ToPersianDate(),actor);

        //    _context.Movie.Add(movie);
        //    _context.SaveChanges();

        //    var result = _context.Movie.FirstOrDefault(i => i.Name == movie.Name);
        //    result.Should().BeEquivalentTo(movie);
        //}
    }
}
