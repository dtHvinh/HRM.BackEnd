using FastEndpoints;
using FluentValidation;

namespace BackEnd.Endpoints.WardEndpoints.Validators;

public class AddWardValidator : Validator<AddWardRequest>
{
    public AddWardValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}
