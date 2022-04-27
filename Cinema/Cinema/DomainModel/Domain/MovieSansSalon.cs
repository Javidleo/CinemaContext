using System;

namespace DomainModel.Domain
{
    public class MovieSansSalon
    {
        public int Id { get; private set; }

        public int MovieId { get; private set; }

        public int SansId { get; private set; }

        public int SalonId { get; private set; }

        public Guid MovieSansSalonGuid { get; private set; }

        public Guid AdminGuid { get; private set; }

        public string AdminFullName { get; private set; }

        public DateTime PremiereDate { get; private set; }

        public string PremiereDatePersian { get; private set; }

        public virtual Movie Movie { get; private set; }

        public virtual Salon Salon { get; private set; }

        public virtual Sans Sans { get; private set; }

        public MovieSansSalon() { }
        private MovieSansSalon(int movieId, int salonId, int sansId, Guid adminGuid, string adminFullName,DateTime premiereDate,
                                string premeiereDatePersian)
        {
            MovieId = movieId;
            SalonId = salonId;
            SansId = sansId;
            AdminGuid = adminGuid;
            AdminFullName = adminFullName;
            PremiereDate = premiereDate;
            PremiereDatePersian = premeiereDatePersian;
        }

        public static MovieSansSalon Create(int movieId, int salonId, int sansId, Guid adminGuid, string adminFullName,
                    DateTime premiereDate,string premiereDatePersian)
        => new(movieId, salonId, sansId, adminGuid, adminFullName,premiereDate, premiereDatePersian);
    }
}
