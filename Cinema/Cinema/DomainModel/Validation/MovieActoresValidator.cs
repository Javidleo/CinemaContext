using DomainModel.Domain;
using FluentValidation;

namespace DomainModel.Validation
{
    public class MovieActoresValidator : AbstractValidator<MovieActores>
    {
        private readonly Base_Validation _base;
        public MovieActoresValidator()
        {
            _base = new Base_Validation();

            RuleFor(i => i.BaseMaleActorName).NotEmpty().WithMessage("baseMaleActorName is empty")
                                .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid baseMaleActorName");

            RuleFor(i => i.BaseFemaleActorName).NotEmpty().WithMessage("baseFemaleActorName is empty")
                                .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid baseFemaleActorName");

            RuleFor(i => i.SupportedMaleActorName).NotEmpty().WithMessage("supportedMaleActorName is empty")
                                .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid supportedMaleActorName");

            RuleFor(i => i.SupportedFemaleActorName).NotEmpty().WithMessage("supportedFemaleActorName is empty")
                                .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid supportedFemaleActorName");
        }
    }
}
