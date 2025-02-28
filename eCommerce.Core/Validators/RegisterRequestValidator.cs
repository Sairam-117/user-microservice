using eCommerce.Core.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Validators;
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(m => m.Email)
       .NotEmpty().WithMessage("Email should not be empty.")
       .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(m => m.Password).NotEmpty().WithMessage("Password should not be empty.")
            .MinimumLength(8).WithMessage("Password should be atleast 8 characters");

        RuleFor(m => m.PersonName).NotEmpty().WithMessage("Person Name cannot be empty")
            .MaximumLength(50).WithMessage("Person Name cannot exceed 50 characters");

        RuleFor(m => m.Gender).IsInEnum();
    }
}
