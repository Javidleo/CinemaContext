using System;
using System.Collections.Generic;

namespace DomainModel.Domain
{
    public class Customer
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Family { get; private set; }

        public string Email { get; private set; }

        public Guid CustomerGuid { get; private set; }

        public virtual List<Ticket> Tickets { get; private set; } = new List<Ticket>();

        private Customer(string name, string family, string email)
        {
            Name = name;
            Family = family;
            Email = email;
        }

        public static Customer Create(string name, string family, string email)
        => new Customer(name, family, email);
    }
}
