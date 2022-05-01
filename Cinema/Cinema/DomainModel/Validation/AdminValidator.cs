using DomainModel.Domain;
using FluentValidation;

namespace DomainModel.Validation
{
    public class AdminValidator : AbstractValidator<Admin>
    {
        private readonly BaseValidation _base;
        public AdminValidator()
        {
            _base = new BaseValidation();

            RuleSet("Name", () =>
            {
                RuleFor(i => i.Name).NotEmpty().WithMessage("name is empty")
                    .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid name");
                RuleFor(i => i.Family).NotEmpty().WithMessage("family is empty")
                    .Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid family");
            });

            RuleSet("UserName&Email", () =>
            {
                RuleFor(i => i.UserName).NotEmpty().WithMessage("userName is empty")
                    .Matches(_base.LowerCaseEnglish_NumbersRegex).WithMessage("invalid userName");

                RuleFor(i => i.Email).NotEmpty().WithMessage("email is empty").EmailAddress().WithMessage("invalid email");

            });

            RuleSet("Password", () =>
            {
                RuleFor(i => i.Password).Must(BaseValidation.CheckPassword).WithMessage("invalid password");
            });

            RuleSet("NationalCode", () =>
            {
                RuleFor(i => i.NationalCode).Must(BaseValidation.CheckNationalCode).WithMessage("invalid nationalCode");
            });
            


        }


    }
}
