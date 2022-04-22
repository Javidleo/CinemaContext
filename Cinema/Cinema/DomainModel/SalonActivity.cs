using System;

namespace DomainModel
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

        private SalonActivity() { }

        public static SalonActivity Create()
        {
            throw new NotImplementedException();
        }
    }
}
