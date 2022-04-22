using DomainModel;

namespace Test.Unit.builders
{
    internal class MovieActoresBuilder
    {
        private string _baseMale = "ali ahmadi";
        private string _baseFemale = "nahid rezaie";
        private string _supportedMale = "reza navidi";
        private string _supportedFemale = "zahra hassani";

        public MovieActoresBuilder WithBaseMaleActor(string name)
        {
            _baseMale = name;
            return this;
        }
        public MovieActoresBuilder WithBaseFemaleActor(string name)
        {
            _baseFemale = name;
            return this;
        }
        public MovieActoresBuilder WithSupportedMaleActor(string name)
        {
            _supportedMale = name;
            return this;
        }
        public MovieActoresBuilder WithSupportedFemaleActor(string name)
        {
            _supportedFemale = name;
            return this;
        }

        public MovieActores Build()
        => MovieActores.Create(_baseMale, _baseFemale, _supportedMale, _supportedFemale);
    }
}