using Api.Infrastructure.Contract;
using Api.Infrastructure.Extensions;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Password;

public class Reset : IEndpoint
{
    private static async Task<IResult> Handler([FromBody] ResetPasswordRequest request,
        [FromServices] IAuthService authService,
        CancellationToken cancellationToken)
    {
        var userId = await authService.FindUserByEmail(request.Email, cancellationToken);
        if (string.IsNullOrEmpty(userId))
            return Results.NotFound();

        var result = await authService.ResetPasswordAsync(userId, request.Email, request.Otp, request.Password, cancellationToken);
        if (result)
            return Results.NoContent();
        
        return Results.BadRequest("invalid_otp");
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("v1/password/reset", Handler)
            .Produces204()
            .WithTags("Password");
    }

    public record ResetPasswordRequest(string Email, string Otp, string Password);
}