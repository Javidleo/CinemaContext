using DomainModel.Domain;

namespace Test.Unit.builders
{
    internal class SansBuilder
    {
        private string _name = "sans1";

        public SansBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Sans Build()
        => Sans.Create(_name);
    }
}