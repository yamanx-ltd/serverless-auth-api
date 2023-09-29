using Domain.Entities;

namespace Domain.Repositories;

public interface IAuthRepository
{
    Task<OtpEntity> CreateLoginOtpAsync(string? userId, string phone, CancellationToken cancellationToken = default);

    Task<OtpEntity?> GetLoginOtpAsync(string phone, string code, CancellationToken cancellationToken = default);

    Task<OtpEntity> CreateForgotPasswordOtpAsync(string? userId, string email, string otp, CancellationToken cancellationToken = default);

    Task<OtpEntity?> GetForgotPasswordOtpAsync(string email, string code, CancellationToken cancellationToken = default);
    Task<RefreshTokenEntity> CreateRefreshTokenAsync(RefreshTokenEntity entity, CancellationToken cancellationToken = default);

    Task<UserPhoneMapEntity?> GetPhoneUserMapAsync(string phone, CancellationToken cancellationToken = default);

    Task<UserPhoneMapEntity> CreatePhoneUserMapAsync(UserPhoneMapEntity entity, CancellationToken cancellationToken = default);
    
    Task<UserEmailMapEntity> CreateEmailUserMapAsync(UserEmailMapEntity entity, CancellationToken cancellationToken = default);
    
    Task<UserPasswordMapEntity> CreatePasswordUserMapAsync(UserPasswordMapEntity entity, CancellationToken cancellationToken = default);

    Task DeleteRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    
    Task<RefreshTokenEntity?> GetRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<UserPasswordMapEntity?> GetPasswordUserMapAsync(string userId, CancellationToken cancellationToken);
    Task<UserEmailMapEntity?> GetEmailUserMapAsync(string email, CancellationToken cancellationToken);
    Task<List<UserPasswordMapEntity>> GetUserPasswords(string userId, CancellationToken cancellationToken);
    Task DeletePasswords(List<UserPasswordMapEntity> olderPasswords, CancellationToken cancellationToken);
}