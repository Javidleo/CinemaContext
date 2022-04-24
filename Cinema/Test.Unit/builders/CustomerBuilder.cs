using DomainModel.Domain;
using System;

namespace Test.Unit.builders
{
    internal class CustomerBuilder
    {
        private string _name = "ali";
        private string _family = "reziae";
        private string _email = "javidleo.ef@gmail.com";

        public CustomerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public CustomerBuilder WithFamily(string family)
        {
            _family = family;
            return this;
        }
        public CustomerBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public Customer Build()
        => Customer.Create(_name, _family, _email);
    }
}