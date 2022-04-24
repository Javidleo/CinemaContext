using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public class Sans
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Guid SansGuid { get; private set; }

        public virtual List<MovieSansSalon> MovieSansSalons { get; private set; } = new List<MovieSansSalon>();

        private Sans(string name)
        {
            Name = name;
        }

        public static Sans Create(string name)
        => new(name);
    }
}
