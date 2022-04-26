using NEGSO.Utilities;
using System;

namespace DomainModel.Domain
{
    public class CinemaActivity
    {
        public int Id { get; private set; }

        public int CinemaId { get; private set; }

        public DateTime StartDate { get; private set; }

        public string StartDatePersian { get; private set; }

        public DateTime? EndDate { get; private set; }

        public string EndDatePersian { get; private set; }

        public string Description { get; private set; }

        public Guid AdminGuid { get; private set; }

        public string AdminFullName { get; private set; }

        public Guid CinemaActivityGuid { get; private set; }

        public virtual Cinema Cinema { get; private set; }

        CinemaActivity() { }
        private CinemaActivity(int cinemaId, DateTime startDate, string persianStartDate
                                ,string description, Guid adminGuid, string adminFullName)
        {
            CinemaId = cinemaId;
            StartDate = startDate;
            StartDatePersian = persianStartDate;
            EndDate = null;
            EndDatePersian = null;
            Description = description;
            AdminGuid = adminGuid;
            AdminFullName = adminFullName;
        }

        public static CinemaActivity Create(int cinemaId, DateTime startDate, string persianStartDate,
                                string description, Guid adminGuid, string adminFullName)
        => new(cinemaId, startDate, persianStartDate, description, adminGuid, adminFullName);

        public void Activate()
        {
            EndDate = DateTime.Now.Date;
            EndDatePersian = DateTime.Now.ToPersianDate();
        }
    }
}
