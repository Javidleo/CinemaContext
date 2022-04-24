using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public class Province
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Guid ProvinceGuid { get; private set; }

        public virtual List<City> Cities { get; private set; } = new List<City>();

        private Province() { }

        public static Province Create()
        {
            throw new NotImplementedException();
        }
    }
}
