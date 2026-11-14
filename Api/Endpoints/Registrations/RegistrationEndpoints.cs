
using Application.Abstractions.Messaging;
using Application.Registrations.CreateRegistration;
using Application.Registrations.GetRegistrations;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Api.Endpoints.Registrations;

public static class RegistrationEndpoints
{
    public static IEndpointRouteBuilder MapRegistrationEndpoints(
        this IEndpointRouteBuilder app)
    {
        // Create Registration
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

        // Get Registrations
        app.MapGet("/api/registrations", async (
            [AsParameters] GetRegistrationsQuery query,
            [FromServices]IQueryHandler<GetRegistrationsQuery,
                Result<GetRegistrationsResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var result = await handler.HandleAsync(
                query, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(new
                {
                    code = result.Error?.Code,
                    message = result.Error?.Message
                });
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetRegistrations")
        .WithTags("Registrations");

        return app;
    }
}
