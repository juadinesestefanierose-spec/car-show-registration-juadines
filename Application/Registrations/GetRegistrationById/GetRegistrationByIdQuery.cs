using Application.Abstractions.Messaging;
using Application.Registrations.CreateRegistration;
using Domain.Common;

namespace Application.Registrations.GetRegistrationById;

public sealed record GetRegistrationByIdQuery(Guid Id)
    : IQuery<Result<RegistrationResponse>>;