using Application.Abstractions.Messaging;
using Domain.Common;

namespace Application.Registrations.GetRegistrationSummary;

public sealed record GetRegistrationSummaryQuery()
    : IQuery<Result<GetRegistrationSummaryResponse>>;