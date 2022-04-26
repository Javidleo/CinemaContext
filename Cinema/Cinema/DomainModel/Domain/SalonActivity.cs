using System;

namespace DomainModel.Domain
{
    public class SalonActivity
    {
        public int Id { get; private set; }

        public int SalonId { get; private set; }

        public DateTime StartDate { get; private set; }

        public string StartDatePersian { get; private set; }

        public DateTime EndDate { get; private set; }

        public string EndDatePersian { get; private set; }

        public string Description { get; private set; }

        public Guid AdminGuid { get; private set; }

        public string AdminFullName { get; private set; }

        public Guid SalonActivityGuid { get; private set; }

        public virtual Salon Salon { get; private set; }

        SalonActivity() { }

        private SalonActivity(int salonId,DateTime startDate,string startDatePersian,string description,Guid adminGuid,
                        string adminFullName) 
        {
            this.SalonId = salonId;
            this.StartDate = startDate;
            this.StartDatePersian = startDatePersian;
            this.Description = description;
            this.AdminGuid = adminGuid;
            this.AdminFullName = adminFullName;

        }

        public static SalonActivity Create(int salonId, DateTime startDate, string startDatePersian, string description, Guid adminGuid,
                        string adminFullName)
        => new(salonId, startDate, startDatePersian, description, adminGuid, adminFullName);
    }
}
