using FluentValidation;

namespace FrontierCRM.Application.Features.Tenants.Commands.CreateTenant;

/// <summary>
/// Validator for CreateTenantCommand
/// </summary>
public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tenant name is required.")
            .MaximumLength(200).WithMessage("Tenant name cannot exceed 200 characters.");

        RuleFor(x => x.Subdomain)
            .MaximumLength(100).WithMessage("Subdomain cannot exceed 100 characters.")
            .Matches(@"^[a-z0-9]([a-z0-9\-]*[a-z0-9])?$")
            .When(x => !string.IsNullOrEmpty(x.Subdomain))
            .WithMessage("Subdomain must contain only lowercase letters, numbers, and hyphens, and cannot start or end with a hyphen.");

        RuleFor(x => x.Website)
            .MaximumLength(500).WithMessage("Website cannot exceed 500 characters.")
            .Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.Website))
            .WithMessage("Website must be a valid URL.");

        RuleFor(x => x.Industry)
            .MaximumLength(100).WithMessage("Industry cannot exceed 100 characters.");

        RuleFor(x => x.Settings)
            .NotNull().WithMessage("Settings are required.");

        RuleFor(x => x.Settings.MaxUsers)
            .GreaterThan(0).WithMessage("Max users must be greater than 0.");

        RuleFor(x => x.Settings.MaxStorageBytes)
            .GreaterThan(0).WithMessage("Max storage bytes must be greater than 0.");

        RuleFor(x => x.Settings.MaxApiCallsPerMonth)
            .GreaterThan(0).WithMessage("Max API calls per month must be greater than 0.");
    }

    private static bool BeAValidUrl(string? url)
    {
        if (string.IsNullOrEmpty(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var result) &&
               (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}
