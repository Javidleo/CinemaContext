using NEGSO.Utilities;
using System;

namespace DomainModel
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

        private CinemaActivity(int cinemaId, DateTime startDate, string persianStartDate, DateTime? endDate, string persianEndDate,
                                string description, Guid adminGuid, string adminFullName)
        {
            this.CinemaId = cinemaId;
            this.StartDate = startDate;
            this.StartDatePersian = persianStartDate;
            this.EndDate = endDate;
            this.EndDatePersian = persianEndDate;
            this.Description = description;
            this.AdminGuid = adminGuid;
            this.AdminFullName = adminFullName;
        }

        public static CinemaActivity Create(int cinemaId, DateTime startDate, string persianStartDate, DateTime? endDate, string persianEndDate,
                                string description, Guid adminGuid, string adminFullName)
        => new(cinemaId, startDate, persianStartDate, endDate, persianEndDate, description, adminGuid, adminFullName);

        public void DeActivate()
        {
            EndDate = DateTime.Now.Date;
            EndDatePersian = DateTime.Now.Date.ToPersianDate();
        }
    }
}
