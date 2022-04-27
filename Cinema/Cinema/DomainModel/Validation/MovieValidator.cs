using DomainModel.Domain;
using FluentValidation;
using System;

namespace DomainModel.Validation
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        private readonly BaseValidation _base;
        public MovieValidator()
        {
            _base = new BaseValidation();

            RuleFor(i => i.AdminGuid).NotEqual(Guid.Empty).WithMessage("adminGuid is empty");

            RuleFor(i => i.AdminFullName).NotEmpty().WithMessage("adminFullName is empty")
                                        .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid adminName");

            RuleFor(i => i.Name).NotEmpty().WithMessage("name is empty")
                                        .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid name");

            RuleFor(i => i.Director).NotEmpty().WithMessage("director is empty")
                                        .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid director");

            RuleFor(i => i.Producer).NotEmpty().WithMessage("producer is empty")
                                        .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid producer");

            RuleFor(i => i.PublishDate).Must(CheckPublishDate).WithMessage("invalid publishDate");
            RuleFor(i => i.PublishDatePersian).Must(CheckPersianPublishDate).WithMessage("invalid persianPublishDate");
        }
        private bool CheckPublishDate(DateTime publishDate)
        {
            if (publishDate > DateTime.Now || publishDate == DateTime.Now.Date) return false;
            if (publishDate == DateTime.MinValue || publishDate == DateTime.MaxValue) return false;

            return true;
        }
        private bool CheckPersianPublishDate(string perisnaPublishDate)
        {
            var date = perisnaPublishDate.Split('/');
            if (date[2].Length != 2 || date[0].Length != 4)
                return false;

            if (date[1].Length == 2 || date[1].Length == 1)
                return true;
            return true;
        }
    }

}
