using DomainModel.Domain;
using FluentValidation;
using System;

namespace DomainModel.Validation
{
    public class CinemaActivityValidator : AbstractValidator<CinemaActivity>
    {
        private readonly BaseValidation _baseValidation;

        public CinemaActivityValidator()
        {
            _baseValidation = new BaseValidation();

            RuleFor(i => i.AdminFullName).NotEmpty().WithMessage("adminFullName is empty")
                .Matches(_baseValidation.Persian_English_WhiteSpaceRegex).WithMessage("invalid adminFullName");

            RuleFor(i => i.AdminGuid).NotEqual(Guid.Empty).WithMessage("adminGuid is empty");
            RuleFor(i => i.StartDatePersian).NotEmpty().WithMessage("startDatePersian is empty")
                .Matches(_baseValidation.PersianDateRegex).WithMessage("invalid startDatePersian");

            //RuleFor(i => i.EndDatePersian).NotEmpty().WithMessage("endDatePersian is empty")
            //    .Matches(_baseValidation.PersianDateRegex).WithMessage("invalid endDatePersian");

            RuleFor(i => i.StartDate).NotEqual(DateTime.MaxValue).WithMessage("invalid startDate")
                .NotEqual(DateTime.MinValue).WithMessage("invalid startDate");
        }
    }
}
