using FastEndpoints;
using FluentValidation;

namespace BackEnd.Endpoints.AllowanceEndpoints.Validators;

public class AddAllowanceValidator : Validator<AddAllowanceRequest>
{
    public AddAllowanceValidator()
    {
        RuleFor(e => e.AllowanceName).NotEmpty().WithMessage("Allowance name is required");
        RuleFor(e => e.AllowanceCoefficient).Must(e => e > 0).WithMessage("Allowance coefficient is must not be negative");
    }
}
