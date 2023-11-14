using Api.Infrastructure.Contract;
using Api.Infrastructure.Extensions;

namespace Api.Endpoints.V1.External.Google;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler()
    {
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("v1/external/google", Handler)
            .Produces200()
            .WithTags("External");
    }
}