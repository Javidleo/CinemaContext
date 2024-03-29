﻿using DomainModel.Domain;

namespace Test.Unit.builders
{
    internal class CityBuilder
    {
        private string _name = "shiraz";
        private int _provinceId = 1;

        public CityBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public CityBuilder WithProvinceId(int id)
        {
            _provinceId = id;
            return this;
        }
        public City Build()
        => City.Create(_name, _provinceId);
    }
}