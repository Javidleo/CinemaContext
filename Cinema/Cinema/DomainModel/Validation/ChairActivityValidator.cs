using DomainModel.Domain;
using FluentValidation;
using System;

namespace DomainModel.Validation
{
    public class ChairActivityValidator : AbstractValidator<ChairActivity>
    {
        private readonly BaseValidation _baseValidation;
        public ChairActivityValidator()
        {
            _baseValidation = new BaseValidation();

            RuleFor(i => i.AdminFullName).NotEmpty().WithMessage("adminFullName is empty")
                .Matches(_baseValidation.Persian_English_WhiteSpaceRegex).WithMessage("invalid adminFullName");

            RuleFor(i => i.AdminGuid).NotEqual(Guid.Empty).WithMessage("adminGuid is empty");
            RuleFor(i => i.StartDate).Must(BaseValidation.CheckDate).WithMessage("invalid startDate");

            RuleFor(i => i.StartDatePersian).NotEmpty().WithMessage("startDatePersian is empty")
                .Matches(_baseValidation.PersianDateRegex).WithMessage("invalid startDatePersian");

        }
    }
}
