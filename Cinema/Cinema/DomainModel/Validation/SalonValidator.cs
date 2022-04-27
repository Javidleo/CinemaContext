using DomainModel.Domain;
using FluentValidation;

namespace DomainModel.Validation
{
    public class SalonValidator : AbstractValidator<Salon>
    {
        private readonly BaseValidation _base;
        public SalonValidator()
        {
            _base = new BaseValidation();

            RuleFor(i => i.Name).NotEmpty().WithMessage("name is empty").Matches(_base.Persian_English_Numbers_WhiteSpaceRegex).WithMessage("invalid name");
            RuleFor(i => i.Capacity).InclusiveBetween(5, 500).WithMessage("invalid capacity");
        }
    }
}
