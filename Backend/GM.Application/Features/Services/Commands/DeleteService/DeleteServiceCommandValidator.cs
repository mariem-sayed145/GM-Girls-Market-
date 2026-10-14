using FluentValidation;

namespace GM.Application.Features.Services.Commands.DeleteService;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Service ID is required.");
    }
}
