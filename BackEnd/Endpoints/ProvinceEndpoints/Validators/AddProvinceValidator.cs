using BackEnd.Endpoints.ProvinceEndpoints;
using FastEndpoints;
using FluentValidation;

namespace BackEnd.Endpoints.ProvinceEndpoints.Validators;

public class AddAccountValidator : Validator<AddProvinceRequest>
{
    public AddAccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}
