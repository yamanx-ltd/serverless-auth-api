using Api.Infrastructure.Contract;
using Api.Infrastructure.Extensions;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Password;

public class Forgot : IEndpoint
{
    private static async Task<IResult> Handler([FromBody] ForgotRequest request,
        [FromServices] IAuthService authService,
        CancellationToken cancellationToken)
    {
        var userId = await authService.FindUserByEmail(request.Email, cancellationToken);
        if (string.IsNullOrEmpty(userId))
            return Results.NotFound();
        
        await authService.SendForgetPasswordOtp(userId, request.Email, cancellationToken);
        return Results.NoContent();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("v1/password/forgot", Handler)
            .Produces204()
            .WithTags("Password");
    }

    public record ForgotRequest(string Email);
}