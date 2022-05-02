using DomainModel.Domain;

namespace Test.Unit.builders
{
    internal class CustomerBuilder
    {
        private string _name = "ali";
        private string _family = "reziae";
        private string _email = "javidleo.ef@gmail.com";
        private string _password = "javidleo!451E1";

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
        public CustomerBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public Customer Build()
        => Customer.Create(_name, _family, _email,_password);
    }
}