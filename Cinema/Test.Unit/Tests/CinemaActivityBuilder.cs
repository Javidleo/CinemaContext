using DomainModel.Domain;
using NEGSO.Utilities;
using System;

namespace Test.Unit.Tests
{
    internal class CinemaActivityBuilder
    {
        private int _cinemaId = 1;
        private DateTime _startDate = DateTime.Now.Date;
        private string _persianStartDate = DateTime.Now.ToPersianDate();
        private DateTime? _endDate = null;
        private string _persianEndDate = new DateTime(2020, 11, 11).ToPersianDate();
        private string _desceription = "new movie added by me ";
        private Guid _adminGuid = Guid.NewGuid();
        private string _adminFullName = "javid";

        public CinemaActivityBuilder WithCinemaId(int id)
        {
            _cinemaId = id;
            return this;
        }
        public CinemaActivityBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }
        public CinemaActivityBuilder WithPersianStartDate(string persianStartDate)
        {
            _persianStartDate = persianStartDate;
            return this;
        }
        public CinemaActivityBuilder WithEndDate(DateTime? endDate)
        {
            _endDate = endDate;
            return this;
        }
        public CinemaActivityBuilder WithPersianEndDate(string persianEndDate)
        {
            _persianEndDate = persianEndDate;
            return this;
        }
        public CinemaActivityBuilder WithDescription(string description)
        {
            _desceription = description;
            return this;
        }
        public CinemaActivityBuilder WithAdminGuid(Guid adminGuid)
        {
            _adminGuid = adminGuid;
            return this;
        }
        public CinemaActivityBuilder WithAdminFullName(string adminFullName)
        {
            _adminFullName = adminFullName;
            return this;
        }
        public CinemaActivity Build()
        => CinemaActivity.Create(_cinemaId, _startDate, _persianStartDate, _endDate, _persianEndDate,
            _desceription, _adminGuid, _adminFullName);
    }
}