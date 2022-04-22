using DomainModel;
using NEGSO.Utilities;
using System;

namespace Test.Unit.builders
{
    internal class MovieBuilder
    {
        private Guid _adminGuid = Guid.NewGuid();
        private string _adminFullName = "admin";
        private string? _name = "something";
        private string? _directorName = "ali";
        private string? _producerName = "reza";
        private DateTime _publishDate = new DateTime(2020, 12, 3).Date;
        private string? _publishDatePersian = new DateTime(2020, 12, 3).ToPersianDate();
        private MovieActores _actor = new MovieActoresBuilder().Build();

        public MovieBuilder WithAdminGuid(Guid adminGuid)
        {
            _adminGuid = adminGuid;
            return this;
        }
        public MovieBuilder WithAdminFullName(string adminName)
        {
            _adminFullName = adminName;
            return this;
        }
        public MovieBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public MovieBuilder WithDirector(string directorName)
        {
            _directorName = directorName;
            return this;
        }
        public MovieBuilder WithProducer(string producerName)
        {
            _producerName = producerName;
            return this;
        }
        public MovieBuilder WithPublishDate(DateTime publishDate)
        {
            _publishDate = publishDate;
            return this;
        }
        public MovieBuilder WithPersianPublishDate(string persianPublishDate)
        {
            _publishDatePersian = persianPublishDate;
            return this;
        }
        public MovieBuilder WithActor(MovieActores actor)
        {
            _actor = actor;
            return this;
        }

        public Movie Build()
        => Movie.Create(_adminGuid, _adminFullName, _name, _directorName, _producerName, _publishDate, _publishDatePersian, _actor);
    }
}