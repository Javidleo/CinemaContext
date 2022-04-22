using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class City
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int ProvinceId { get; private set; }

        public Guid CityGuid { get; private set; }

        public virtual List<Cinema> Cinemas { get; private set; } = new List<Cinema>();

        public virtual Province Province { get; private set; }

        private City() { }

        public static City Create()
        {
            throw new NotImplementedException();
        }
    }
}
