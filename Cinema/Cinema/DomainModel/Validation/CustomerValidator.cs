using DomainModel.Domain;
using FluentValidation;

namespace DomainModel.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly Base_Validation _base;
        public CustomerValidator()
        {
            _base = new Base_Validation();
            
            RuleFor(i => i.Name).NotEmpty().WithMessage("name is empty").Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid name");
            RuleFor(i => i.Family).NotEmpty().WithMessage("family is empty").Matches(_base.Persian_English_WhiteSpaceRegex).WithMessage("invalid family");
            RuleFor(i => i.Email).NotEmpty().WithMessage("email is empty").EmailAddress().WithMessage("invalid email");
        }
    }
}
