using FluentValidation;

namespace GM.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Project ID is invalid.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(200).WithMessage("Project name must not exceed 200 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Project description is required.")
            .MaximumLength(4000).WithMessage("Project description must not exceed 4000 characters.");

        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("Client ID is required.");
    }
}
