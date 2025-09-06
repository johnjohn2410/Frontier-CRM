using FluentValidation;

namespace FrontierCRM.Application.Features.Users.Commands.CreateUser;

/// <summary>
/// Validator for CreateUserCommand
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.")
            .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters.");

        RuleFor(x => x.Phone)
            .MaximumLength(50).WithMessage("Phone cannot exceed 50 characters.")
            .Matches(@"^[\+]?[1-9][\d]{0,15}$")
            .When(x => !string.IsNullOrEmpty(x.Phone))
            .WithMessage("Phone must be a valid phone number.");

        RuleFor(x => x.JobTitle)
            .MaximumLength(200).WithMessage("Job title cannot exceed 200 characters.");

        RuleFor(x => x.Department)
            .MaximumLength(100).WithMessage("Department cannot exceed 100 characters.");

        RuleFor(x => x.Preferences)
            .NotNull().WithMessage("Preferences are required.");

        RuleFor(x => x.Preferences.ItemsPerPage)
            .GreaterThan(0).WithMessage("Items per page must be greater than 0.")
            .LessThanOrEqualTo(1000).WithMessage("Items per page cannot exceed 1000.");
    }
}
