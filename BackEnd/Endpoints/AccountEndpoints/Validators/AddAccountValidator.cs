using FastEndpoints;
using FluentValidation;

namespace BackEnd.Endpoints.AccountEndpoints.Validators;

public class AddAccountValidator : Validator<AddAccountRequest>
{
    public AddAccountValidator()
    {
        RuleFor(e => e.Username).NotEmpty().WithMessage("Username can not be empty");
        RuleFor(e => e.Password).NotEmpty().WithMessage("Username can not be empty");
    }
}
