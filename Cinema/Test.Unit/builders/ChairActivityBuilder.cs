using DomainModel.Domain;
using NEGSO.Utilities;
using System;

namespace Test.Unit.builders
{
    internal class ChairActivityBuilder
    {
        private int _chairId = 1;
        private string _adminFullName = "javid";
        private Guid _adminGuid = Guid.NewGuid();
        private DateTime _startDate = DateTime.Now.Date;
        private string _startDatePersian = DateTime.Now.Date.ToPersianDate();
        private string _description = "descritpion";

        public ChairActivityBuilder WithChairId(int chairId)
        {
            _chairId = chairId;
            return this;
        }
        public ChairActivityBuilder WithAdminFullName(string adminFullName)
        {
            _adminFullName = adminFullName;
            return this;
        }
        public ChairActivityBuilder WithAdminGuid(Guid adminGuid)
        {
            _adminGuid = adminGuid;
            return this;
        }
        public ChairActivityBuilder WithStartDate(DateTime date)
        {
            _startDate = date;
            return this;
        }
        public ChairActivityBuilder WithStartDatePersian(string persianDate)
        {
            _startDatePersian = persianDate;
            return this;
        }
        public ChairActivityBuilder WithDescritption(string description)
        {
            _description = description;
            return this;
        }

        public ChairActivity Build()
        => ChairActivity.Create(_chairId, _startDate, _startDatePersian, _description, _adminGuid, _adminFullName);
    }
}