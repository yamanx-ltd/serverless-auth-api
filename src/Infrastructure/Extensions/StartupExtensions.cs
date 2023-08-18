using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.Configuration.SystemsManager;
using Amazon.Extensions.NETCore.Setup;
using Domain.Options;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, ConfigurationManager configuration)
    {
        service.AddScoped<IAuthService, AuthService>();
        service.AddSingleton<IAuthRepository, AuthRepository>();
        service.AddScoped<IMessageService, MessageService>();
        service.AddSingleton<IMessageRepository, MessageRepository>();
        service.AddScoped<ICryptoService, CryptoService>();
        service.AddScoped<ISmsProviderFactory, SmsProviderFactory>();
        service.AddScoped<ISmsProvider, NetGsmSmsProvider>();
        service.AddScoped<ISmsProvider, TwilioSmsProvider>();
        service.AddScoped<IJwtService, JwtService>();
        service.AddAWSService<IAmazonDynamoDB>();
        service.AddAWSLambdaHosting(LambdaEventSource.RestApi);
        service.AddScoped<IApiContext, ApiContext>();

        service.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        service.Configure<PasswordSalt>(configuration.GetSection("PasswordSalt"));

        var smsSection = configuration.GetSection("SmsProviders");
        service.Configure<NetGsmOptions>(smsSection.GetSection("NetGsm"));
        service.Configure<TwilioOptions>(smsSection.GetSection("Twilio"));


        configuration.AddSystemsManager(config =>
        {
            config.Path = $"/auth-api";
            config.ReloadAfter = TimeSpan.FromMinutes(5);
            config.ParameterProcessor = new JsonParameterProcessor();
            config.AwsOptions = new AWSOptions
            {
                Profile = "serverless",
                Region = RegionEndpoint.EUWest1
            };
        });
        return service;
    }
}