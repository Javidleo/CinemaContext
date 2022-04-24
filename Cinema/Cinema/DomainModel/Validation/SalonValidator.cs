using DomainModel.Domain;
using FluentValidation;

namespace DomainModel.Validation
{
    public class SalonValidator : AbstractValidator<Salon>
    {
        private readonly Base_Validation _base;
        public SalonValidator()
        {
            _base = new Base_Validation();

            RuleFor(i => i.Name).NotEmpty().WithMessage("name is empty").Matches(_base.Persian_English_Numbers_WhiteSpaceRegex).WithMessage("invalid name");
            RuleFor(i => i.Capacity).ExclusiveBetween(5, 2000).WithMessage("invalid capacity");
        }
    }
}
