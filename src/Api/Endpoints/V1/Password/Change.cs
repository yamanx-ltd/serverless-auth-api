using Api.Infrastructure.Contract;
using Domain.Services;
using FluentValidation;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Password;

public class Change : IEndpoint
{
    private static async Task<IResult> Handler([FromBody] ChangePasswordRequest request,
        [FromServices] IApiContext apiContext,
        [FromServices] IAuthService authService,
        [FromServices] IValidator<ChangePasswordRequest> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.ToDictionary());

        var checkUserPassword = await authService.CheckUserPassword(apiContext.CurrentUserId, request.OldPassword, cancellationToken);
        if (!checkUserPassword)
            return Results.Forbid();

        await authService.CreatePasswordUserMapping(apiContext.CurrentUserId, request.NewPassword, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/v1/password/change", Handler)
            .WithTags("Password");
    }

    public record ChangePasswordRequest(string OldPassword, string NewPassword);

    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(q => q.NewPassword).NotEmpty();
            RuleFor(q => q.OldPassword).NotEmpty();
        }
    }
}