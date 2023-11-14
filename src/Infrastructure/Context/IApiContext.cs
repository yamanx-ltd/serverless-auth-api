using Microsoft.AspNetCore.Http;

namespace Infrastructure.Context
{
    public interface IApiContext
    {
        string CurrentUserId { get; }
        string Culture { get; }
        string? Channel { get; }

        string? IpAddress { get; }
    }

    public class ApiContext : IApiContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CurrentUserId => ReadFromHeader("x-user-id") ?? throw new Exception("User id not found");
        public string Culture => ReadFromHeader("x-culture") ?? "en-US";
        public string? Channel => ReadFromHeader("x-channel");

        public string? IpAddress
        {
            get
            {
                var ip = ReadFromHeader("cf-connecting-ip");
                if (!string.IsNullOrEmpty(ip))
                    return ip;

                //get Ip from request
                if (_httpContextAccessor.HttpContext?.Connection.RemoteIpAddress != null)
                    return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                return null;
            }
        }

        private string? ReadFromHeader(string headerName)
        {
            if (_httpContextAccessor.HttpContext == null)
                return null;

            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerName, out var value))
                return value.ToString();
            return null;
        }
    }
}