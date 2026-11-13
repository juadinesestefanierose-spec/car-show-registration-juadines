
using Application.Abstractions.Messaging;
using Application.Registrations.CreateRegistration;
using Domain.Common;
using Microsoft.AspNetCore.Routing;

namespace Api.Endpoints.Registrations;

public static class RegistrationEndpoints
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/registrations", async (
            CreateRegistrationCommand command,
            ICommandHandler<CreateRegistrationCommand,
                Result<RegistrationResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(
                command, cancellationToken);

            if (!result.IsSuccess)
            {
                if (result.Error?.Code == "Registration.DuplicatePlate")
                {
                    return Results.Conflict(new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message
                    });
                }

                return Results.BadRequest(new
                {
                    code = result.Error?.Code,
                    message = result.Error?.Message
                });
            }

            return Results.Created(
                $"/api/registrations/{result.Value!.Id}",
                result.Value);
        })
        .WithName("CreateRegistration")
        .WithTags("Registrations");

        return app;
    }
}
