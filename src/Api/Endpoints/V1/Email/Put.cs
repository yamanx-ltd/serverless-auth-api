using Api.Infrastructure.Contract;
using Api.Infrastructure.Extensions;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Email;

public class Put : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string userId,
        [FromBody] UpdateUserEmailMappingRequest request,
        [FromServices] IAuthService authService,
        CancellationToken cancellationToken)
    {
        await authService.UpdateUserEmailMappingAsync(userId, request.Email, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPut("/v1/users/{userId}/email", Handler)
            .Produces200()
            .Produces400()
            .Produces404()
            .Produces500()
            .WithTags("User");
    }

    public record UpdateUserEmailMappingRequest(string Email);
}