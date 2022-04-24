using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public class Movie
    {
        public int Id { get; private set; }

        public Guid AdminGuid { get; private set; }

        public string AdminFullName { get; private set; }

        public string Name { get; private set; }

        public string Director { get; private set; }

        public string Producer { get; private set; }

        public DateTime PublishDate { get; private set; }

        public string PublishDatePersian { get; private set; }

        public Guid MovieGuid { get; private set; }

        public virtual List<MovieSansSalon> MovieSansSalons { get; private set; } = new List<MovieSansSalon>();

        public virtual List<MovieActores> MovieActores { get; private set; } = new List<MovieActores>();

        private Movie(Guid adminGuid, string adminFullName, string name, string director, string producer, DateTime publishDate
                    , string publishDatePersian, MovieActores actor)
        {
            AdminGuid = adminGuid;
            AdminFullName = adminFullName;
            Name = name;
            Producer = producer;
            Director = director;
            PublishDate = publishDate;
            PublishDatePersian = publishDatePersian;
            MovieActores.Add(actor);
        }

        public static Movie Create(Guid adminGuid, string adminFullName, string name, string director, string producer,
                                    DateTime publishDate, string publishDatePersian, MovieActores actor)
        => new(adminGuid, adminFullName, name, director, producer, publishDate, publishDatePersian, actor);
    }
}
