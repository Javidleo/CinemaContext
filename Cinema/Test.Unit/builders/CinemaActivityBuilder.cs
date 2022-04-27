using DomainModel.Domain;
using NEGSO.Utilities;
using System;

namespace Test.Unit.builders
{
    internal class CinemaActivityBuilder
    {
        private int _cinemaId = 1;
        private DateTime _startDate = DateTime.Now.Date;
        private string _persianStartDate = DateTime.Now.ToPersianDate();
        private string _desceription = "some problem in power";
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
        => CinemaActivity.Create(_cinemaId, _startDate, _persianStartDate, _desceription, _adminGuid, _adminFullName);
    }
}