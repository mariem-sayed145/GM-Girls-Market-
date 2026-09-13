using FluentValidation;

namespace GM.Application.Features.ContactInquiries.Commands.CreateContactInquiry;

public class CreateContactInquiryCommandValidator
    : AbstractValidator<CreateContactInquiryCommand>
{
    public CreateContactInquiryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        RuleFor(x => x.Subject)
            .NotEmpty()
            .WithMessage("Subject is required.");

        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Message is required.");
    }
} 