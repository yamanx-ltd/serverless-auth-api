using Api.Infrastructure.Contract;
using Api.Infrastructure.Extensions;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1;

public class Delete : IEndpoint
{
    private async Task<IResult> Handler([FromBody] DeleteUserRequest request, 
        [FromServices] IAuthService authService,
        CancellationToken cancellationToken)
    {

        await authService.DeleteAllUserDataAsync(request.Id, request.Email, request.Phone, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("v1/users", Handler)
            .Produces200()
            .WithTags("User");
    }
}

public record DeleteUserRequest(string Id, string Email, string Phone);