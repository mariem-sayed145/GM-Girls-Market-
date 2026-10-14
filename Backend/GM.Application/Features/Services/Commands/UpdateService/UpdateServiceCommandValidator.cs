using FluentValidation;

namespace GM.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Service ID is invalid.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Service name is required.")
            .MaximumLength(200)
            .WithMessage("Service name cannot exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Service description is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Service price must be greater than zero.");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Service category is required.");
    }
}
