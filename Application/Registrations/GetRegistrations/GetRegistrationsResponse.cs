
using Application.Registrations.CreateRegistration;

namespace Application.Registrations.GetRegistrations;

public sealed record GetRegistrationsResponse(
    IReadOnlyList<RegistrationResponse> Items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages
);
