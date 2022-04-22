using NEGSO.Utilities;
using System;

namespace DomainModel
{
    public class MovieSansSalon
    {
        public int Id { get; private set; }

        public int MovieId { get; private set; }

        public int SansId { get; private set; }

        public int SalonId { get; private set; }

        public Guid AdminGuid { get; private set; }

        public DateTime CreationDate { get; private set; }

        public string CreationDatePersian { get; private set; }

        public virtual Movie Movie { get; private set; }

        public virtual Salon Salon { get; private set; }

        public virtual Sans Sans { get; private set; }

        private MovieSansSalon(int movieId,int salonId,int sansId,Guid adminGuid)
        {
            MovieId = movieId;
            SalonId = salonId;
            SansId = sansId;
            AdminGuid = adminGuid;
            CreationDate = DateTime.Now.Date;
            CreationDatePersian = DateTime.Now.ToPersianDate();
        }

        public static MovieSansSalon Create(int movieId, int salonId, int sansId, Guid adminGuid)
        => new(movieId, salonId, sansId, adminGuid);
    }
}
