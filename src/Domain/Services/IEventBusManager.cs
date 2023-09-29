using Domain.Entities;

namespace Domain.Services;

public interface IEventBusManager
{
    Task<bool> LoginOtpRequestedAsync(string? userId, string phone, string code, CancellationToken cancellationToken);
    Task<bool> ForgetPasswordOtpRequestedAsync(string userId, string code, CancellationToken cancellationToken);
}