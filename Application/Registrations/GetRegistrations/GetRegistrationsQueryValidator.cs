using FluentValidation;

namespace Application.Registrations.GetRegistrations;

public sealed class GetRegistrationsQueryValidator
    : AbstractValidator<GetRegistrationsQuery>
{
    public GetRegistrationsQueryValidator()
    {
        RuleFor(x => x.ParticipantName)
            .MaximumLength(150)
            .When(x => !string.IsNullOrWhiteSpace(x.ParticipantName));

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50);
    }
}