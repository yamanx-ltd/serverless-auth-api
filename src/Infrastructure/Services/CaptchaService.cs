using System.Net.Http.Json;
using System.Text.Json;
using Domain.Domains.Cloudflare.Captcha;
using Domain.Domains.Google.Captcha;
using Domain.Options;
using Domain.Services;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class CaptchaService : ICaptchaService
{
    private readonly IOptionsSnapshot<CaptchaOptions> _captchaOptions;
    private static readonly HttpClient HttpClient = new();

    public CaptchaService(IOptionsSnapshot<CaptchaOptions> captchaOptions)
    {
        _captchaOptions = captchaOptions;
    }

    public async Task<bool> ValidateAsync(string token, string? ip, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("CaptchaService.ValidateAsync");
        Console.WriteLine(JsonSerializer.Serialize(_captchaOptions.Value));
        if (_captchaOptions.Value.Google?.IsEnabled == true)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _captchaOptions.Value.Google.ValidationUrl);
            var payload = new Dictionary<string, string>
            {
                {"secret", _captchaOptions.Value.Google.ServerKey},
                {"response", token},
            };
            if (!string.IsNullOrEmpty(ip))
                payload.Add("remoteip", ip);
            httpRequest.Content = new FormUrlEncodedContent(payload);
            var responseModel = HttpClient.PostAsync(httpRequest.RequestUri, httpRequest.Content, cancellationToken);
            var response = await responseModel.Result.Content.ReadFromJsonAsync<GoogleCaptchaResponseModel>(cancellationToken: cancellationToken);
            return response?.Success == true;
        }

        if (_captchaOptions.Value.Cloudflare?.IsEnabled == true)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _captchaOptions.Value.Cloudflare.ValidationUrl);
            var payload = new Dictionary<string, string>
            {
                {"secret", _captchaOptions.Value.Cloudflare.SecretKey},
                {"response", token},
            };
            if (!string.IsNullOrEmpty(ip))
                payload.Add("remoteip", ip);
            httpRequest.Content = new FormUrlEncodedContent(payload);
            var responseModel = HttpClient.PostAsync(httpRequest.RequestUri, httpRequest.Content, cancellationToken);
            var response = await responseModel.Result.Content.ReadFromJsonAsync<CloudflareCaptchaResponseModel>(cancellationToken: cancellationToken);
            return response?.Success == true;
        }

        return true;
    }
}