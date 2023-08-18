using Domain.Domains;

namespace Domain.Services;

public interface IJwtService
{
    Task<JwtDto> CreateJwtAsync(string userId, CancellationToken cancellationToken = default);
    Task<string?> ValidateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken= default);
}