using DomainModel.Domain;
using System;

namespace Test.Unit.builders
{
    internal class MovieSansSalonBuilder
    {
        private int _movieId = 1;
        private int _salonId = 1;
        private int _sansId = 1;
        private Guid _adminGuid = Guid.NewGuid();
        private string _adminFullName = "javid hasani";
        private DateTime _premiereDate = DateTime.Now.Date;
        private string _premiereDatePersian = "1399/11/12";

        public MovieSansSalonBuilder WithMovieId(int movieId)
        {
            _movieId = movieId;
            return this;
        }
        public MovieSansSalonBuilder WithSalonId(int salonId)
        {
            _salonId = salonId;
            return this;
        }
        public MovieSansSalonBuilder WithSansId(int sansId)
        {
            _sansId = sansId;
            return this;
        }
        public MovieSansSalonBuilder WithAdminGuid(Guid adminGuid)
        {
            _adminGuid = adminGuid;
            return this;
        }
        public MovieSansSalonBuilder WithAdminFullName(string adminFullName)
        {
            _adminFullName = adminFullName;
            return this;
        }
        public MovieSansSalonBuilder WithPremiereDate(DateTime date)
        {
            _premiereDate = date;
            return this;
        }
        public MovieSansSalonBuilder WithPremiereDatePersian(string persianDate)
        {
            _premiereDatePersian = persianDate;
            return this;
        }

        public MovieSansSalon Build()
        => MovieSansSalon.Create(_movieId, _salonId, _sansId, _adminGuid, _adminFullName,_premiereDate,_premiereDatePersian);
    }
}