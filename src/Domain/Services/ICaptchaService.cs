namespace Domain.Services;

public interface ICaptchaService
{
    Task<bool> ValidateAsync(string token, string? ip, CancellationToken cancellationToken = default);
}