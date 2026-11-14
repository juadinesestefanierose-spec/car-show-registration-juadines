
using Application.Abstractions.Messaging;
using Application.Registrations.CreateRegistration;
using Application.Registrations.GetRegistrationById;
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
        // CREATE REGISTRATION
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

        // GET ALL REGISTRATIONS
        app.MapGet("/api/registrations", async (
            [AsParameters] GetRegistrationsQuery query,
            [FromServices] IQueryHandler<GetRegistrationsQuery,
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

        // GET REGISTRATION BY ID
        app.MapGet("/api/registrations/{id:guid}", async (
            Guid id,
            [FromServices] IQueryHandler<GetRegistrationByIdQuery,
                Result<RegistrationResponse>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetRegistrationByIdQuery(id);

            var result = await handler.HandleAsync(
                query, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.NotFound(new
                {
                    code = result.Error?.Code,
                    message = result.Error?.Message
                });
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetRegistrationById")
        .WithTags("Registrations");

        return app;
    }
}
