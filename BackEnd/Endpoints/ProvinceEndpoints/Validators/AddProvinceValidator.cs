using BackEnd.Endpoints.ProvinceEndpoints;
using FastEndpoints;
using FluentValidation;

namespace BackEnd.Endpoints.ProvinceEndpoints.Validators;

public class AddProvinceValidator : Validator<AddProvinceRequest>
{
    public AddProvinceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}
