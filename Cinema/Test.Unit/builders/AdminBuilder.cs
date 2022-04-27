using DomainModel.Domain;

namespace Test.Unit.builders
{
    internal class AdminBuilder
    {
        private int _cinemaId = 1;
        private string _name = "علی رضا";
        private string _family = "رضایی";
        private string _nationalCode = "0119241560";
        private string _email = "javidleo.ef@gmail.com";
        private string _userName = "javidleo";
        private string _password = "Jfds243rfwer";

        public AdminBuilder WithCinemaId(int id)
        {
            _cinemaId = id;
            return this;
        }
        public AdminBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public AdminBuilder WithFamily(string family)
        {
            _family = family;
            return this;
        }
        public AdminBuilder WithNationalCode(string nationalCode)
        {
            this._nationalCode = nationalCode;
            return this;
        }
        public AdminBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }
        public AdminBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }
        public AdminBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public Admin Build()
        => Admin.Create(_cinemaId, _name, _family, _nationalCode, _email, _userName, _password);
    }
}