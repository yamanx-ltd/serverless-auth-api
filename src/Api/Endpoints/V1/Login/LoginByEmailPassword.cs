using Api.Infrastructure.Contract;
using Api.Infrastructure.Extensions;
using Domain.Domains;
using Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Login;

public class LoginByEmailPassword : IEndpoint
{
    private static async Task<IResult> Handler([FromBody] LoginByEmailRequest request,
        [FromServices] IAuthService authService,
        [FromServices] IJwtService jwtService,
        [FromServices] IValidator<LoginByEmailRequest> validator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Results.BadRequest(validationResult.ToDictionary());

        var userId = await authService.FindUserByEmail(request.Email.ToLower(), cancellationToken);
        if (string.IsNullOrEmpty(userId))
            return Results.NotFound();

        var isPasswordValid = await authService.CheckUserPassword(userId, request.Password, cancellationToken);
        if (!isPasswordValid)
            return Results.BadRequest(new Dictionary<string, string[]> {{"EmailOrPassword", new[] {"Invalid Email or Password"}}});

        var jwt = await jwtService.CreateJwtAsync(userId, cancellationToken);
        return Results.Ok(jwt);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("v1/login", Handler)
            .Produces200<JwtDto>()
            .WithTags("Login");
    }

    public record LoginByEmailRequest(string Email, string Password);

    public class LoginByEmailRequestValidator : AbstractValidator<LoginByEmailRequest>
    {
        public LoginByEmailRequestValidator()
        {
            RuleFor(q => q.Email).NotEmpty().EmailAddress();
            RuleFor(q => q.Password).NotEmpty();
        }
    }
}