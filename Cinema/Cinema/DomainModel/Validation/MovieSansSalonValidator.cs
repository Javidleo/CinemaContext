using DomainModel.Domain;
using FluentValidation;
using System;

namespace DomainModel.Validation
{
    public class MovieSansSalonValidator : AbstractValidator<MovieSansSalon>
    {
        private readonly Base_Validation _base;
        public MovieSansSalonValidator()
        {
            _base = new Base_Validation();

            RuleFor(i => i.AdminFullName).NotEmpty().WithMessage("adminFullName is empty")
                    .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid adminFullName");

            RuleFor(i => i.AdminGuid).NotEqual(Guid.Empty).WithMessage("invalid adminGuid");
            RuleFor(i => i.PremiereDate).NotEqual(DateTime.MinValue).NotEqual(DateTime.MaxValue).WithMessage("invalid premiereDate");
            RuleFor(i => i.PremiereDatePersian).Matches(_base.PersianDateRegex).WithMessage("invalid persianDate");
        }
    }
}
