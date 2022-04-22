using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Sans
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Guid SansGuid { get; private set; }

        public virtual List<MovieSansSalon> MovieSansSalons { get; private set; } = new List<MovieSansSalon>();

        private Sans() { }

        public static Sans Create()
        {
            throw new NotImplementedException();
        }
    }
}
