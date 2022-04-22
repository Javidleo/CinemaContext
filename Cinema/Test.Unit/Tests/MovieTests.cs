using DomainModel.Validation;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Test.Unit.builders;
using Test.Unit.TestDoubles;
using UseCases.Exceptions;
using UseCases.ServiceContract;
using UseCases.Services;
using Xunit;

namespace Test.Unit.Tests
{
    public class MovieTests
    {
        private readonly IMovieService _movieService;
        private readonly MovieValidator _validator;
        private readonly MovieFakeRepository _movieFakeRepository;
        private readonly AdminFakeRepository _adminFakeRepository;
        public MovieTests()
        {
            _movieFakeRepository = new MovieFakeRepository();
            _adminFakeRepository = new AdminFakeRepository();
            _movieService = new MovieService(_movieFakeRepository, _adminFakeRepository);
            _validator = new MovieValidator();
        }

        [Fact]
        public void MovieValidation_CheckForNullName_ThorwValidationError()
        {
            var movie = new MovieBuilder().WithName("").Build();

            var result = _validator.TestValidate(movie);
            result.ShouldHaveValidationErrorFor(i => i.Name).WithErrorMessage("name is empty");
        }

        [Fact]
        public void MovieValidation_CheckForNullDirectorName_ThrowValidationError()
        {
            var movie = new MovieBuilder().WithDirector("").Build();

            var result = _validator.TestValidate(movie);
            result.ShouldHaveValidationErrorFor(i => i.Director).WithErrorMessage("director is empty");
        }

        [Fact]
        public void MovieValidation_CheckForNullProducer_ThrowValidationError()
        {
            var movie = new MovieBuilder().WithProducer("").Build();

            var result = _validator.TestValidate(movie);
            result.ShouldHaveValidationErrorFor(i => i.Producer).WithErrorMessage("producer is empty");
        }

        [Fact]
        public void MovieValidation_CheckForMoreThanBiggestPossibleDate_ThrowValidationError()
        {
            var movie = new MovieBuilder().WithPublishDate(DateTime.Now.AddYears(1)).Build();

            var result = _validator.TestValidate(movie);
            result.ShouldHaveValidationErrorFor(i => i.PublishDate).WithErrorMessage("invalid publishDate");
        }

        [Fact]
        public void MovieValidation_CheckForLessThanNowDate_ThrowValidationError()
        {
            var movie = new MovieBuilder().WithPublishDate(DateTime.Now.Date).Build();

            var result = _validator.TestValidate(movie);
            result.ShouldHaveValidationErrorFor(i => i.PublishDate).WithErrorMessage("invalid publishDate");
        }

        [Fact]
        public void MovieValidateion_CheckForMaxValueForDateTime_ThrowValidationError()
        {
            var movie = new MovieBuilder().WithPublishDate(DateTime.MaxValue).Build();

            var result = _validator.TestValidate(movie);
            result.ShouldHaveValidationErrorFor(i => i.PublishDate).WithErrorMessage("invalid publishDate");
        }

        [Fact]
        public void MovieValidation_CheckForMinValeForDateTime_ThorwsValdiateionError()
        {
            var movie = new MovieBuilder().WithPublishDate(DateTime.MinValue).Build();

            var result = _validator.TestValidate(movie);
            result.ShouldHaveValidationErrorFor(i => i.PublishDate).WithErrorMessage("invalid publishDate");
        }

        //[Theory]
        //[InlineData("11/123/1399")]
        //[InlineData("11/12/973412")]
        //[InlineData("12341/42/123")]
        //[InlineData("123/12/12")]
        //public void MovieValidatioin_CheckForInvaidPersianDate_ThorwValidationError(string date)
        //{
        //    var movie = new MovieBuilder().WithPersianPublishDate(date).Build();

        //    var result = _validator.TestValidate(movie);
        //    result.ShouldHaveValidationErrorFor(i => i.PublishDatePersian).WithErrorMessage("invalid persian publishDate");
        //}

        [Fact]
        public void CreateMovie_CheckForWorkingWell()
        {
            var actor = new MovieActoresBuilder().Build();
            var movie = new MovieBuilder().WithActor(actor).Build();
            var result = _movieService.Create(movie.AdminGuid, movie.AdminFullName, movie.Name, movie.Director, movie.Producer, movie.PublishDate
                                              , actor.BaseMaleActorName, actor.BaseFemaleActorName, actor.SupportedMaleActorName
                                              , actor.SupportedFemaleActorName);

            result.Status.ToString().Should().Be("RanToCompletion");
        }

        [Theory]
        [InlineData("", "nahid", "navid", "mona")]
        [InlineData("reza", "", "navid", "mona")]
        [InlineData("reza", "nahid", "", "mona")]
        [InlineData("reza", "nahid", "navid", "")]
        [InlineData("reza$", "nahid", "navid", "mona")]
        [InlineData("reza", "nahid!", "navid", "mona")]
        [InlineData("reza", "nahid", "navid_2", "mona")]
        [InlineData("reza", "nahid", "navid", "mona2")]
        public void CreateMovie_CheckForMovieActorValidation_ThrowsNotAcceptableException(string baseMale, string baseFemale, string suppMale, string suppFemale)
        {
            var movie = new MovieBuilder().Build();
            var actor = new MovieActoresBuilder()
                            .WithBaseMaleActor(baseMale)
                            .WithBaseFemaleActor(baseFemale)
                            .WithSupportedMaleActor(suppMale)
                            .WithSupportedFemaleActor(suppFemale).Build();

            void result() => _movieService.Create(movie.AdminGuid, movie.AdminFullName, movie.Name, movie.Director, movie.Producer,
                                             movie.PublishDate, actor.BaseMaleActorName, actor.BaseFemaleActorName, actor.SupportedMaleActorName
                                            , actor.SupportedFemaleActorName);

            var exception = Assert.Throws<NotAcceptableException>(result);
            exception.Message.Should().Be("invalid movie actors");
        }

        [Theory]
        [InlineData("", "director", "producer")]
        [InlineData("movie", "", "producer")]
        [InlineData("movie", "director", "")]
        [InlineData("movi35%#", "director", "producer")]
        [InlineData("movie", "dire%^", "producer")]
        [InlineData("movie", "director", "123$123")]
        public void CreateMovie_CheckForMovieValidation_ThrowsNotAcceptableException(string name, string directorName, string producerName)
        {
            var movie = new MovieBuilder().WithName(name).WithDirector(directorName).WithProducer(producerName).Build();
            var actor = new MovieActoresBuilder().Build();

            void result() => _movieService.Create(movie.AdminGuid, movie.AdminFullName, movie.Name, movie.Director, movie.Producer,
                                             movie.PublishDate, actor.BaseMaleActorName, actor.BaseFemaleActorName, actor.SupportedMaleActorName
                                            , actor.SupportedFemaleActorName);

            var exception = Assert.Throws<NotAcceptableException>(result);
            exception.Message.Should().Be("invalid movie");
        }
    }
}
