using Domain.Constants;
using Domain.Entities;
using Domain.Options;
using Domain.Repositories;
using Domain.Services;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IOptionsSnapshot<JwtOptions> _jwtOptionsSnapshot;
    private readonly IMessageService _messageService;
    private readonly ISmsProviderFactory _smsProviderFactory;
    private readonly ICryptoService _cryptoService;

    public AuthService(IAuthRepository authRepository, IOptionsSnapshot<JwtOptions> jwtOptionsSnapshot, IMessageService messageService, ISmsProviderFactory smsProviderFactory, ICryptoService cryptoService)
    {
        _authRepository = authRepository;
        _jwtOptionsSnapshot = jwtOptionsSnapshot;
        _messageService = messageService;
        _smsProviderFactory = smsProviderFactory;
        _cryptoService = cryptoService;
    }

    public async Task<bool> SendLoginOtpAsync(string? userId, string phone, string culture, CancellationToken cancellationToken = default)
    {
        var otpEntity = await _authRepository.CreateLoginOtpAsync(userId, phone, cancellationToken);
        var message = await _messageService.GetMessageAsync(culture, MessageKeys.OTPSms, cancellationToken);

        var messagePayload = $"Code {otpEntity.Otp}";
        if (message != null)
        {
            messagePayload = string.Format(message.Message, otpEntity.Otp);
        }

        return await _smsProviderFactory.SendSms(phone, messagePayload, cancellationToken);
    }

    public async Task<string?> FindUserByPhone(string phone, CancellationToken cancellationToken)
    {
        var entity = await _authRepository.GetPhoneUserMapAsync(phone, cancellationToken);
        return entity?.UserId;
    }

    public async Task<string?> FindUserByEmail(string email, CancellationToken cancellationToken = default)
    {
        var entity = await _authRepository.GetEmailUserMapAsync(email, cancellationToken);
        return entity?.UserId;
    }

    public async Task<bool> CheckUserPassword(string userId, string password, CancellationToken cancellationToken = default)
    {
        var entity = await _authRepository.GetPasswordUserMapAsync(userId, cancellationToken);

        return entity?.Password == _cryptoService.HashPassword(password);
    }

    public async Task<bool> VerifyOtpAsync(string phone, string otp, CancellationToken cancellationToken)
    {
        var entity = await _authRepository.GetLoginOtpAsync(phone, otp, cancellationToken);
        return entity != null;
    }

    public async Task CreateRefreshTokenAsync(string userId, string token, CancellationToken cancellationToken)
    {
        var entity = new RefreshTokenEntity
        {
            UserId = userId,
            RefreshToken = token,
            ExpireAt = DateTime.UtcNow.AddDays(_jwtOptionsSnapshot.Value.RefreshExpireDays)
        };

        await _authRepository.CreateRefreshTokenAsync(entity, cancellationToken);
    }

    public async Task DeleteRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        await _authRepository.DeleteRefreshTokenAsync(refreshToken, cancellationToken);
    }

    public async Task<bool> CreatePhoneUserMapping(string phone, string userId, CancellationToken cancellationToken = default)
    {
        var entity = new UserPhoneMapEntity()
        {
            Phone = phone,
            UserId = userId
        };

        await _authRepository.CreatePhoneUserMapAsync(entity, cancellationToken);
        return true;
    }

    public async Task CreateEmailUserMapping(string email, string userId, CancellationToken cancellationToken)
    {
        await _authRepository.CreateEmailUserMapAsync(new UserEmailMapEntity
        {
            UserId = userId,
            Email = email
        }, cancellationToken);
    }

    public async Task CreatePasswordUserMapping(string userId, string password, CancellationToken cancellationToken)
    {
        List<UserPasswordMapEntity> olderPasswords = await _authRepository.GetUserPasswords(userId, cancellationToken);
        await _authRepository.DeletePasswords(olderPasswords, cancellationToken);
        await _authRepository.CreatePasswordUserMapAsync(new UserPasswordMapEntity
        {
            UserId = userId,
            Password = _cryptoService.HashPassword(password)
        }, cancellationToken);
    }
}