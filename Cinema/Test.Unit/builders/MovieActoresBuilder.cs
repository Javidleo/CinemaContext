using DomainModel.Domain;
using System;

namespace Test.Unit.builders
{
    internal class MovieActoresBuilder
    {
        private string _baseMale = "ali ahmadi";
        private string _baseFemale = "nahid rezaie";
        private string _supportedMale = "reza navidi";
        private string _supportedFemale = "zahra hassani";
        private Guid _adminGuid = Guid.NewGuid();
        private string _adminFullName = "adminfullname";

        public MovieActoresBuilder WithBaseMaleActor(string name)
        {
            _baseMale = name;
            return this;
        }
        public MovieActoresBuilder WithBaseFemaleActor(string name)
        {
            _baseFemale = name;
            return this;
        }
        public MovieActoresBuilder WithSupportedMaleActor(string name)
        {
            _supportedMale = name;
            return this;
        }
        public MovieActoresBuilder WithSupportedFemaleActor(string name)
        {
            _supportedFemale = name;
            return this;
        }
        public MovieActoresBuilder WithAdminGuid(Guid adminGuid)
        {
            _adminGuid = adminGuid;
            return this;
        }
        public MovieActoresBuilder WithAdminFullName(string adminFullName)
        {
            this._adminFullName = adminFullName;
            return this;
        }

        public MovieActores Build()
        => MovieActores.Create(_baseMale, _baseFemale, _supportedMale, _supportedFemale, _adminGuid, _adminFullName);
    }
}