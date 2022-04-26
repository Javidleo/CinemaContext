using System;

namespace DomainModel.Domain
{
    public class ChairActivity
    {
        public int Id { get; private set; }

        public int ChairId { get; private set; }

        public DateTime StartDate { get; private set; }

        public string StartDatePersian { get; private set; }

        public DateTime EndDate { get; private set; }

        public string EndDatePersian { get; private set; }

        public string Description { get; private set; }

        public Guid ChairActivityGuid { get; private set; }

        public Guid AdminGuid { get; private set; }

        public string AdminFullName { get; private set; }

        public virtual Chair Chair { get; private set; }

        ChairActivity() { }

        private ChairActivity(int chairId, DateTime startDate, string startDatePersian,string description, Guid adminGuid, string adminFullName)
        {
            ChairId = chairId;
            StartDate = startDate;
            StartDatePersian = startDatePersian;
            Description = description;
            AdminGuid = adminGuid;
            AdminFullName = adminFullName;
        }

        public static ChairActivity Create(int chairId, DateTime startDate, string startDatePersian,
                    string description, Guid adminGuid, string adminFullName)
        => new(chairId, startDate, startDatePersian,description, adminGuid, adminFullName);
    }
}
