using Api.Infrastructure.Contract;
using Api.Infrastructure.Extensions;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Phone;

public class Put : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string userId,
        [FromBody] UpdateUserPhoneMappingRequest request,
        [FromServices] IAuthService authService,
        CancellationToken cancellationToken)
    {
        await authService.UpdateUserPhoneMappingAsync(userId, request.OldPhone, request.Phone, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPut("/v1/users/{userId}/phone", Handler)
            .Produces200()
            .Produces400()
            .Produces404()
            .Produces500()
            .WithTags("User");
    }

    public record UpdateUserPhoneMappingRequest(string? OldPhone, string Phone);
}