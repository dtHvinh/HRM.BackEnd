using FastEndpoints;
using FluentValidation;

namespace BackEnd.Endpoints.InsuranceEndpoints.Validators;

public class InsuranceValidator : Validator<AddInsuranceRequest>
{
    public InsuranceValidator()
    {
        RuleFor(e => e.InsuranceName).NotEmpty().WithMessage("Insurance name is required");
        RuleFor(e => e.InsuranceCoefficient).Must(e => e > 0).WithMessage("Insurance coefficient is must not be negative");
    }
}
