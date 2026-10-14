using FluentValidation;

namespace GM.Application.Features.CustomerRequests.Commands.CreateCustomerRequest;

public class CreateCustomerRequestCommandValidator : AbstractValidator<CreateCustomerRequestCommand>
{
    public CreateCustomerRequestCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("User ID is invalid.");

        RuleFor(x => x.ServiceId)
            .GreaterThan(0)
            .WithMessage("Service ID is invalid.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(2000)
            .WithMessage("Description cannot exceed 2000 characters.");
    }
}
