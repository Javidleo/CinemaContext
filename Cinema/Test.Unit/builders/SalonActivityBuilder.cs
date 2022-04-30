using DomainModel.Domain;
using NEGSO.Utilities;
using System;

namespace Test.Unit.builders
{
    internal class SalonActivityBuilder
    {
        private int _salonId = 1;
        private DateTime _startDate = DateTime.Now.Date;
        private string _startDatePersian = DateTime.Now.ToPersianDate();
        private string _description = "close for fixing";
        private Guid _adminGuid = Guid.NewGuid();
        private string _adminFullName = "javid";

        public SalonActivityBuilder WithSalonId(int salonId)
        {
            _salonId = salonId;
            return this;
        }
        public SalonActivityBuilder WithStartDate(DateTime date)
        {
            _startDate = date;
            return this;
        }
        public SalonActivityBuilder WithStartDatePersian(string persianDate)
        {
            _startDatePersian = persianDate;
            return this;
        }
        public SalonActivityBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
        public SalonActivityBuilder WithAdminGuid(Guid adminGuid)
        {
            _adminGuid = adminGuid;
            return this;
        }
        public SalonActivityBuilder WithAdminFullName(string adminFullName)
        {
            _adminFullName = adminFullName;
            return this;
        }

        public SalonActivity Build()
        => SalonActivity.Create(_salonId, _startDate, _startDatePersian, _description, _adminGuid, _adminFullName);
    }
}